using System.Threading.Tasks;
using Terraria;

namespace WikiBrowser.Requests {
    public class TerrariaRequest {
        private Task<string> _task;
        private static readonly HttpRequest HttpRequest = new HttpRequest();

        private static string GetItemTask(Task<string> data) {
            var item = Helpers.GetTrueItemName(data.Result);
            var task = HttpRequest.Get(item, Helpers.BaseUri, Helpers.RequestType.GetItem);
            return task.Result;
        }

        public void GetItem(Item item) {
            _task = HttpRequest.Get(item.Name, Helpers.BaseUri, Helpers.RequestType.Search)
                .ContinueWith(GetItemTask);
        }

        public bool IsDone() {
            return _task.IsCompleted;
        }

        public Result Result() {
            if (!IsDone()) return new Result("Awaiting result");
            var res = _task.Result;

            var extract = Helpers.GetExtract(res);
            var title = Helpers.GetTitle(res);

            if (extract != null && title != null) return new Result(title, extract);

            return new Result("Title or extract may be null", "Something is very wrong");
        }
    }
}
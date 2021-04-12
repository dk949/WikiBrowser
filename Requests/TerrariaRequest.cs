using System.Threading.Tasks;

namespace WikiBrowser.Requests {
    public class TerrariaRequest {
        private Task<string> _task;

        private static string GetItemTask(Task<string> data) {
            var item = Helpers.GetTrueItemName(data.Result);
            if (item == null) {
                // This is very hacky, I am making a fake Json string Which should get processed like all the others
                return
                    @"{""extract"":""                   ¯\\_(⊙^⊙)_/¯           "", ""title"":""Page could not be found.""}";
            }

            var task = HttpRequest.Get(item, Helpers.BaseUri, Helpers.RequestType.GetItem);
            return task.Result;
        }

        public void GetItem(string item) {
            _task = HttpRequest.Get(item, Helpers.BaseUri, Helpers.RequestType.Search)
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
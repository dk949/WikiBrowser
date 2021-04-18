using System.Threading.Tasks;
using Terraria;

namespace WikiBrowser.Requests {
    public abstract class Request {
        protected Task<string> Task;
        protected abstract string GetBody(string res);
        protected abstract string GetTitle(string res);

        public abstract void GetItem(Item item);
        public abstract void GetItem(string item);

        public bool IsDone() {
            return Task.IsCompleted;
        }

        public Result Result() {
            if (!IsDone()) return new Result("Awaiting result");
            var res = Task.Result;

            var extract = GetBody(res);
            var title = GetTitle(res);

            if (extract != null && title != null) return new Result(title, extract);

            return new Result("Title or extract may be null", "Something is very wrong");
        }
    }
}
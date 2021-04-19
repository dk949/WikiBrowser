using Terraria;

namespace WikiBrowser.Requests {
    internal class WikiRequest : HttpRequest {
        public override void GetItem(Item item) {
            GetItem(item.Name);
        }

        public override void GetItem(string item) {
            Task = Get(item, Helpers.BaseUri, Helpers.RequestType.Search)
                .ContinueWith(GetItemTask);
        }

        protected override string GetBody(string res) => Helpers.GetExtract(res);
        protected override string GetTitle(string res) => Helpers.GetTitle(res);
    }
}
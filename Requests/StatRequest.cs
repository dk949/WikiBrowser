using System.Threading.Tasks;
using Terraria;

namespace WikiBrowser.Requests {
    internal abstract class StatRequest : Request {
        protected override string GetBody(string res) {
            return res.Split('&')[1];
        }

        protected override string GetTitle(string res) {
            return res.Split('&')[0];
        }

        public override void GetItem(Item item) {
            Task = Get(item);
        }

        public override void GetItem(string item) {
            throw new System.NotImplementedException();
        }

        protected abstract Task<string> Get(Item item);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria;

namespace WikiBrowser.Requests {
    public class TerrariaRequest : HttpRequest {
        private Task<string> _task;

        public TerrariaRequest() {
            BaseUri = "https://terraria.fandom.com/api.php";
        }


        public bool IsDone() {
            return _task.IsCompleted;
        }

        public string Result() {
            if (IsDone()) {
                var a = ((JObject) JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(_task.Result)["query"]);
                var b = a.SelectToken("$..extract").ToString();
                return b;
            }

            return IsDone() ? _task.Result : "Awaiting result";
        }


        public void GetItem(Item item) {
            _task = Get(item.Name);
        }
    }
}
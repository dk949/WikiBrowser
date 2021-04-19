using System.Net.Http;
using System.Threading.Tasks;

namespace WikiBrowser.Requests {
    internal abstract class HttpRequest : Request {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static Task<string> Get(string item, string baseUri, Helpers.RequestType type) {
            var uri = Helpers.FormUri(item, baseUri, type);
            return Helpers.GetStringFromHttp(uri, HttpClient);
        }

        internal static string GetItemTask(Task<string> data) {
            var item = Helpers.GetTrueItemName(data.Result);
            if (item == null) {
                // This is very hacky, I am making a fake Json string Which should get processed like all the others
                return
                    @"{""extract"":""                   ¯\\_(⊙^⊙)_/¯           "", ""title"":""Page could not be found.""}";
            }

            var task = Get(item, Helpers.BaseUri, Helpers.RequestType.GetItem);
            return task.Result;
        }
    }
}
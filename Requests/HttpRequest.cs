using System.Net.Http;
using System.Threading.Tasks;
using static WikiBrowser.Logging;

namespace WikiBrowser.Requests {
    public class HttpRequest {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static Task<string> Get(string item, string baseUri, Helpers.RequestType type) {
            Log(item ?? "Item is null in Get", LogType.Info);
            Log(baseUri ?? "baseUri is null in Get", LogType.Info);
            var uri = Helpers.FormUri(item, baseUri, type);
            return Helpers.GetStringFromHttp(uri, HttpClient);
        }
    }
}
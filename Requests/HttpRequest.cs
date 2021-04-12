using System.Net.Http;
using System.Threading.Tasks;

namespace WikiBrowser.Requests {
    public class HttpRequest {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static Task<string> Get(string item, string baseUri, Helpers.RequestType type) {
            var uri = Helpers.FormUri(item, baseUri, type);
            return Helpers.GetStringFromHttp(uri, HttpClient);
        }
    }
}
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace WikiBrowser.Requests {
    public class TerrariaRequest {
        private const string BaseUri = "https://terraria.fandom.com/api.php";
        private static readonly HttpClient HttpClient = new HttpClient();
        private Task<string> _task;

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


        public void GetItem(Item item) {
            var uri = Helpers.FormUri(item.Name, BaseUri);
            _task = GetStringFromHttp(uri);
        }


        protected async Task<string> GetStringFromHttp(string uri) {
            ModContent.GetInstance<WikiBrowser>().Logger
                .Info("####################Starting the http task######################");
            try {
                ModContent.GetInstance<WikiBrowser>().Logger.Info(uri);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return await HttpClient.GetStringAsync(uri);
            } catch (HttpRequestException e) {
                ModContent.GetInstance<WikiBrowser>().Logger.Error("http error: " + e.StackTrace);
            } catch (Exception e) {
                ModContent.GetInstance<WikiBrowser>().Logger.Error("unknown error: " + e.Message);
            }

            return null;
        }
    }
}
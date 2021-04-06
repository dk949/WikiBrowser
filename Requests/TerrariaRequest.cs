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

        public string Result() {
            if (IsDone()) {
                var extract =
                    Helpers.GetExtract(_task.Result);
                return extract ?? "Extract is null?";
            }

            return "Awaiting result";
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
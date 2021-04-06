using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace WikiBrowser.Requests {
    public abstract class HttpRequest {
        private static readonly HttpClient HttpClient = new HttpClient();
        protected string BaseUri;

        private Dictionary<string, string> ParamDict = new Dictionary<string, string>() {
            {"action", "query"},
            {"format", "json"},
            {"maxlag", ""},
            {"prop", "extracts"},
            {"continue", ""},
            {"rawcontinue", "1"},
            {"titles", "Feather"},
            {"explaintext", "1"},
            {"exlimit", "1"}
        };

        private string FormatString(string item) {
            return item.Replace(' ', '_');
        }

        private string FormUri(string item, string baseUri) {
            ParamDict["titles"] = FormatString(item);

            StringBuilder sb = new StringBuilder(baseUri);
            sb.Append('?');
            foreach (var param in ParamDict) {
                sb.AppendFormat("{0}={1}&", param.Key, param.Value);
            }

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
            
        }

        protected async Task<string> GetStringFromHttp(string uri) {
            try {
                ModContent.GetInstance<WikiBrowser>().Logger.Info(uri);
                return await HttpClient.GetStringAsync(uri);
            } catch (HttpRequestException e) {
                ModContent.GetInstance<WikiBrowser>().Logger.Error("http error: " + e.Message);
            } catch (Exception e) {
                ModContent.GetInstance<WikiBrowser>().Logger.Error("unknown error: " + e.Message);
            }

            return null;
        }


        protected async Task<string> Get(string item) {
            return await GetStringFromHttp(FormUri(item, BaseUri));
        }
    }
}
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WikiBrowser.Requests {
    public class Helpers {
        private static readonly Dictionary<string, string> ParamDict = new Dictionary<string, string> {
            {"action", "query"},
            {"format", "json"},
            {"maxlag", ""},
            {"prop", "extracts"},
            {"continue", ""},
            {"rawcontinue", "1"},
            {"explaintext", "1"},
            {"exlimit", "1"}
        };

        public static string GetExtract(string json) {
            return JsonConvert.DeserializeObject<JObject>(json).SelectToken("$..extract").ToString();
        }


        public static string FormatItemName(string item) {
            return item.Replace(' ', '_');
        }


        public static string FormUri(string item, string baseUri) {
            var sb = new StringBuilder(baseUri);
            sb.Append('?');
            foreach (var param in ParamDict) sb.AppendFormat("{0}={1}&", param.Key, param.Value);

            sb.AppendFormat("titles={0}", FormatItemName(item));
            return sb.ToString();
        }

        public static string GetTitle(string json) {
            return JsonConvert.DeserializeObject<JObject>(json).SelectToken("$..title").ToString();
        }
    }
}
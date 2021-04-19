using Terraria.ModLoader;
using WikiBrowser.UI;
using WikiBrowser.Requests;
using static WikiBrowser.Logging;

namespace WikiBrowser.Commands {
    public class WikiCommand : ModCommand {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "wiki";

        public override string Description
            => "";

        public override void Action(CommandCaller caller, string input, string[] args) {
            Log("Making UI visible", LogType.Info);
            if (args.Length > 0) {
                var req = string.Join(" ", args);
                var item = Helpers.ItemFromName(req);
                if (item != null) {
                    ModContent.GetInstance<WikiBrowser>().UiState.PerformItemRequest(item);
                } else {
                    ModContent.GetInstance<WikiBrowser>().UiState.PerformRequest(req);
                }
            }

            MainUiState.Visible = true;
        }
    }
}
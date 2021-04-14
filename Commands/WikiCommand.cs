using Terraria.ModLoader;
using WikiBrowser.UI;
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
                ModContent.GetInstance<WikiBrowser>().UiState.PerformRequest(req);
            }

            MainUiState.Visible = true;
        }
    }
}
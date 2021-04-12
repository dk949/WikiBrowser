using Terraria.ModLoader;
using WikiBrowser.UI;
using static WikiBrowser.Logging;

namespace WikiBrowser.Commands {
    public class CoinCommand : ModCommand {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "test";

        public override string Description
            => "Show the coin rate UI";

        public override void Action(CommandCaller caller, string input, string[] args) {
            Log("Making UI visible", LogType.Info);
            MainUIState.Visible = true;
        }
    }
}
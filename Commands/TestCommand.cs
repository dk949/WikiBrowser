using Terraria.ModLoader;
using WikiBrowser.UI;

namespace WikiBrowser.Commands {
    public class CoinCommand : ModCommand {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "test";

        public override string Description
            => "Show the coin rate UI";

        public override void Action(CommandCaller caller, string input, string[] args) {
            ModContent.GetInstance<WikiBrowser>().Logger.Info("Ui should be visible now");
            MainUIState.Visible = true;
        }
    }
}
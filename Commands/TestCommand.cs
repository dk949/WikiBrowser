using WikiBrowser.UI;
using Terraria.ModLoader;

namespace WikiBrowser.Commands {

	public class CoinCommand : ModCommand {
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "test";

		public override string Description
			=> "Show the coin rate UI";

		public override void Action(CommandCaller caller, string input, string[] args) {
			MainUIState.Visible = true;
		}
	}
}

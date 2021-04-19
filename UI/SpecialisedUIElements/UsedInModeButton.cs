using static WikiBrowser.UI.UiConfig;

namespace WikiBrowser.UI.SpecialisedUIElements {
    internal class UsedInModeButton : UiRadioButton {
        private const string Text = "Used in";

        public UsedInModeButton(MouseEvent callback) : base(Text, callback) {
            Left.Set(UsedInMode.InitLeft, 0);
            Top.Set(UsedInMode.InitTop, 0);
            Width.Set(UsedInMode.Width, 0);
            Height.Set(UsedInMode.Height, 0);
        }
    }
}
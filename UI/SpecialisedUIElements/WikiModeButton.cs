using static WikiBrowser.UI.UiConfig;

namespace WikiBrowser.UI.SpecialisedUIElements {
    internal class WikiModeButton : UiRadioButton {
        private const string Text = "Wiki";

        public WikiModeButton(MouseEvent callback) : base(Text, callback) {
            IsSelected = true;
            Left.Set(WikiMode.InitLeft, 0);
            Top.Set(WikiMode.InitTop, 0);
            Width.Set(WikiMode.Width, 0);
            Height.Set(WikiMode.Height, 0);
        }
    }
}
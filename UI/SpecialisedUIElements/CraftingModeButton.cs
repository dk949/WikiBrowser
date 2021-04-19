using static WikiBrowser.UI.UiConfig;

namespace WikiBrowser.UI.SpecialisedUIElements {
    internal class CraftingModeButton : UiRadioButton {
        private const string Text = "Crafted from";

        public CraftingModeButton(MouseEvent callback) : base(Text, callback) {
            Left.Set(CraftingMode.InitLeft, 0);
            Top.Set(CraftingMode.InitTop, 0);
            Width.Set(CraftingMode.Width, 0);
            Height.Set(CraftingMode.Height, 0);
        }
    }
}
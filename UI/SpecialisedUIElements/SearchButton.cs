using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using static WikiBrowser.UI.UiConfig;

namespace WikiBrowser.UI.SpecialisedUIElements {
    internal class SearchButton : UiHoverImageButton {
        private static readonly Texture2D
            Texture = ModContent.GetTexture("Terraria/Item_216"); // This is a magnifying glass


        // TODO: add localisation instead of the word "search"
        public SearchButton(MouseEvent callback) : base(Texture, "Search") {
            Left.Set(Request.InitLeft, 0f);
            Top.Set(Request.InitTop, 0f);
            Width.Set(Request.Width, 0f);
            Height.Set(Request.Height, 0f);
            OnClick += callback;
        }
    }
}
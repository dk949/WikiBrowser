using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.ModLoader;
using static WikiBrowser.UI.UiConfig;

namespace WikiBrowser.UI.SpecialisedUIElements {
    internal class CloseButton : UiHoverImageButton {
        private static readonly Texture2D Texture = ModContent.GetTexture("Terraria/Item_2735"); // this is an X

        public CloseButton(MouseEvent callback) : base(Texture, Language.GetTextValue("LegacyInterface.52")) {
            Left.Set(Close.InitLeft, 0f);
            Top.Set(Close.InitTop, 0f);
            Width.Set(Close.Width, 0f);
            Height.Set(Close.Height, 0f);
            OnClick += callback;
        }
    }
}
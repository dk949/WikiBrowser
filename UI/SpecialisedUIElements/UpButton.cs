using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using static WikiBrowser.UI.UiConfig;

namespace WikiBrowser.UI.SpecialisedUIElements {
    internal class UpButton : UIImageButton {
        private static readonly Texture2D Texture = ModContent.GetTexture(Textures.PageUp);

        public UpButton(MouseEvent callback) : base(Texture) {
            // TODO: there is a naming conflict which means I have to specify the UiConfig class
            Left.Set(UiConfig.UpButton.InitLeft, 0f);
            Top.Set(UiConfig.UpButton.InitTop, 0f);
            Width.Set(UiConfig.UpButton.Width, 0f);
            Height.Set(UiConfig.UpButton.Height, 0f);
            OnClick += callback;
        }
    }
}
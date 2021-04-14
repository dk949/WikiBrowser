using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using static WikiBrowser.UI.UiConfig;

namespace WikiBrowser.UI.SpecialisedUIElements {
    internal class DownButton : UIImageButton {
        private static readonly Texture2D Texture = ModContent.GetTexture(Textures.PageDown);

        public DownButton(MouseEvent callback) : base(Texture) {
            // TODO: there is a naming conflict which means I have to specify the UiConfig class
            Left.Set(UiConfig.DownButton.InitLeft, 0f);
            Top.Set(UiConfig.DownButton.InitTop, 0f);
            Width.Set(UiConfig.DownButton.Width, 0f);
            Height.Set(UiConfig.DownButton.Height, 0f);
            OnClick += callback;
        }
    }
}
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace WikiBrowser.UI {
    // This class was borrowed from th tModLoader Example Mod
    internal class UiHoverImageButton : UIImageButton {
        internal string HoverText;

        public UiHoverImageButton(Texture2D texture, string hoverText) : base(texture) {
            HoverText = hoverText;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);

            if (IsMouseHovering) {
                Main.hoverItemName = HoverText;
            }
        }
    }
}
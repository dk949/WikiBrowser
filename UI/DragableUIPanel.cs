using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace WikiBrowser.UI {
    // This class was borrowed from th tModLoader Example Mod
    internal class DragableUIPanel : UIPanel {
        private Vector2 _offset;
        public bool Dragging;

        public override void MouseDown(UIMouseEvent evt) {
            base.MouseDown(evt);
            DragStart(evt);
        }

        public override void MouseUp(UIMouseEvent evt) {
            base.MouseUp(evt);
            DragEnd(evt);
        }

        private void DragStart(UIMouseEvent evt) {
            _offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            Dragging = true;
        }

        private void DragEnd(UIMouseEvent evt) {
            Vector2 end = evt.MousePosition;
            Dragging = false;

            Left.Set(end.X - _offset.X, 0f);
            Top.Set(end.Y - _offset.Y, 0f);

            Recalculate();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime); // don't remove.

            if (ContainsPoint(Main.MouseScreen)) {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (Dragging) {
                Left.Set(Main.mouseX - _offset.X, 0f);
                Top.Set(Main.mouseY - _offset.Y, 0f);
                Recalculate();
            }

            var parentSpace = Parent.GetDimensions().ToRectangle();
            if (GetDimensions().ToRectangle().Intersects(parentSpace)) return;
            Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
            Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
            Recalculate();
        }
    }
}
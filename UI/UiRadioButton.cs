using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace WikiBrowser.UI {
    internal class UiRadioButton : UiHoverImageButton {
        private static readonly Texture2D TextureInactive = ModContent.GetTexture("Terraria/Item_4242");
        private static readonly Texture2D TextureActive = ModContent.GetTexture("Terraria/Item_4243");
        private bool _isSelected;

        public bool IsSelected {
            get => _isSelected;
            set {
                SetImage(value ? TextureActive : TextureInactive);
                _isSelected = value;
            }
        }

        public UiRadioButton(string hoverText, MouseEvent callback) : base(TextureInactive, hoverText) {
            OnClick += callback;
            OnClick += (evt, element) => { IsSelected = true; };
        }
    }
}
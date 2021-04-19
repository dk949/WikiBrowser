using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace WikiBrowser.UI {
    internal class UiModeSelector : UIElement {
        private readonly List<UiRadioButton> _buttons;

        public int CurrentlySelected { get; private set; }


        internal UiModeSelector() {
            _buttons = new List<UiRadioButton>();
        }

        public void Add(UiRadioButton button) => _buttons.Add(button);

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            for (var i = 0; i < _buttons.Count; i++) {
                if (!_buttons[i].IsSelected || i == CurrentlySelected) continue;
                _buttons[CurrentlySelected].IsSelected = false;
                CurrentlySelected = i;
            }
        }

        public void Reset() {
            _buttons[0].IsSelected = true;
            for (var i = 1; i < _buttons.Count; i++) {
                _buttons[i].IsSelected = false;
            }
        }
    }
}
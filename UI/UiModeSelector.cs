using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.UI;
using static WikiBrowser.UI.UiConfig;

namespace WikiBrowser.UI {
    internal class UiModeSelector : UIElement {
        private readonly List<UiRadioButton> _buttons;
        private int _currentlySelected = 0;


        internal UiModeSelector() {
            _buttons = new List<UiRadioButton>();
        }

        public void Add(UiRadioButton button) => _buttons.Add(button);

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            for (int i = 0; i < _buttons.Count; i++) {
                if (!_buttons[i].IsSelected || i == _currentlySelected) continue;
                _buttons[_currentlySelected].IsSelected = false;
                _currentlySelected = i;
            }
        }
    }
}
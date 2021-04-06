using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using WikiBrowser.Requests;
using Player = On.Terraria.Player;

namespace WikiBrowser.UI {
    // MainUIState's visibility is toggled by typing "/test" in chat. (See TestCommand.cs)
    // MainUIState is a simple UI example showing how to use UIPanel, UIImageButton, and even a custom UIElement.
    internal class MainUIState : UIState {
        private TerrariaRequest _request;

        private DragableUIPanel _mainPanel;

        private UIHoverImageButton _sendRequestButton;
        private VanillaItemSlotWrapper _vanillaItemSlot;
        private UIText _text;
        public static bool Visible;

        // In OnInitialize, we place various UIElements onto our UIState (this class).
        // UIState classes have width and height equal to the full screen, because of this, usually we first define a UIElement that will act as the container for our UI.
        // We then place various other UIElement onto that container UIElement positioned relative to the container UIElement.
        public override void OnInitialize() {
            _request = new TerrariaRequest();

            // Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features.
            _mainPanel = new DragableUIPanel();
            _mainPanel.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(coinCounterPanel);`. 
            // This means that this class, MainUIState, will be our Parent. Since MainUIState is a UIState, the Left and Top are relative to the top left of the screen.
            _mainPanel.Left.Set(400f, 0f);
            _mainPanel.Top.Set(100f, 0f);
            _mainPanel.Width.Set(170f, 0f);
            _mainPanel.Height.Set(170f, 0f);
            _mainPanel.BackgroundColor = new Color(73, 94, 171);


            Texture2D closeTexture = ModContent.GetTexture(Textures.closeButton);
            UIHoverImageButton closeButton =
                new UIHoverImageButton(closeTexture,
                    Language.GetTextValue("LegacyInterface.52"));
            closeButton.Left.Set(140, 0f);
            closeButton.Top.Set(10, 0f);
            closeButton.Width.Set(22, 0f);
            closeButton.Height.Set(22, 0f);
            closeButton.OnClick += new MouseEvent(CloseButtonClicked);
            _mainPanel.Append(closeButton);

            Texture2D upTexture = ModContent.GetTexture(Textures.VoteUp);
            _sendRequestButton = new UIHoverImageButton(upTexture, "Send request");
            _sendRequestButton.Left.Set(140, 0f);
            _sendRequestButton.Top.Set(36, 0f);
            _sendRequestButton.Width.Set(22, 0f);
            _sendRequestButton.Height.Set(22, 0f);
            _sendRequestButton.OnClick += new MouseEvent(RequestButtonClicked);
            _mainPanel.Append(_sendRequestButton);


            _vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f);
            _vanillaItemSlot.Left.Set(20, 0);
            _vanillaItemSlot.Top.Set(170 - _vanillaItemSlot.Height.Pixels, 0);
            _vanillaItemSlot.ValidItemFunc = item => true;

            _mainPanel.Append(_vanillaItemSlot);


            _text = new UIText("Here is a different text", 1F, false);
            _text.Left.Set(20, 0);
            _text.Top.Set(260, 0);
            _text.Height.Set(70, 0);
            _text.Width.Set(150, 0);
            _mainPanel.Append(_text);

            Append(_mainPanel);

            // As a recap, ExampleUI is a UIState, meaning it covers the whole screen. We attach coinCounterPanel to ExampleUI some distance from the top left corner.
            // We then place playButton, closeButton, and moneyDiplay onto coinCounterPanel so we can easily place these UIElements relative to coinCounterPanel.
            // Since coinCounterPanel will move, this proper organization will move playButton, closeButton, and moneyDiplay properly when coinCounterPanel moves.
        }


        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuClose);
            if (!_vanillaItemSlot.Item.IsAir) {
                // QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
                Main.LocalPlayer.QuickSpawnClonedItem(_vanillaItemSlot.Item, _vanillaItemSlot.Item.stack);
                // Now that we've spawned the item back onto the player, we reset the item by turning it into air.
                _vanillaItemSlot.Item.TurnToAir();
            }

            _text.SetText("");
            Visible = false;
        }

        private void RequestButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
            // This was something to do with ModPlayer
            Main.NewText("Sending Request");
            if (_vanillaItemSlot.Item.IsAir) {
                _text.SetText("No Item");
            } else {
                _request.GetItem(_vanillaItemSlot.Item);
                Task task = Task.Run(() => {
                    while (!_request.IsDone()) {
                        _text.SetText("Loading...");
                    }

                    _text.SetText(_request.Result());
                });
            }
        }
    }
}
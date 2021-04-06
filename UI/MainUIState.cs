using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using WikiBrowser.Requests;

namespace WikiBrowser.UI {
    // MainUIState's visibility is toggled by typing "/test" in chat. (See TestCommand.cs)
    // MainUIState is a simple UI example showing how to use UIPanel, UIImageButton, and even a custom UIElement.
    internal class MainUIState : UIState {
        public static bool Visible;

        private DragableUIPanel _mainPanel;
        private TerrariaRequest _request;

        private UIHoverImageButton _sendRequestButton;
        private UIText _text;
        private VanillaItemSlotWrapper _vanillaItemSlot;

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


            var closeTexture = ModContent.GetTexture(Textures.closeButton);
            var closeButton =
                new UIHoverImageButton(closeTexture,
                    Language.GetTextValue("LegacyInterface.52"));
            closeButton.Left.Set(140, 0f);
            closeButton.Top.Set(10, 0f);
            closeButton.Width.Set(22, 0f);
            closeButton.Height.Set(22, 0f);
            closeButton.OnClick += CloseButtonClicked;
            _mainPanel.Append(closeButton);

            var upTexture = ModContent.GetTexture(Textures.VoteUp);
            _sendRequestButton = new UIHoverImageButton(upTexture, "Send request");
            _sendRequestButton.Left.Set(140, 0f);
            _sendRequestButton.Top.Set(36, 0f);
            _sendRequestButton.Width.Set(22, 0f);
            _sendRequestButton.Height.Set(22, 0f);
            _sendRequestButton.OnClick += RequestButtonClicked;
            _mainPanel.Append(_sendRequestButton);


            _vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f);
            _vanillaItemSlot.Left.Set(20, 0);
            _vanillaItemSlot.Top.Set(170 - _vanillaItemSlot.Height.Pixels, 0);
            _vanillaItemSlot.ValidItemFunc = item => true;

            _mainPanel.Append(_vanillaItemSlot);


            _text = new UIText("Here is a different text");
            _text.Left.Set(20, 0);
            _text.Top.Set(260, 0);
            _text.Height.Set(70, 0);
            _text.Width.Set(150, 0);
            _mainPanel.Append(_text);

            Append(_mainPanel);
        }


        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuClose);
            if (!_vanillaItemSlot.Item.IsAir) {
                // QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
                Main.LocalPlayer.QuickSpawnClonedItem(_vanillaItemSlot.Item, _vanillaItemSlot.Item.stack);
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
                ModContent.GetInstance<WikiBrowser>().Logger
                    .Info("####################Http task should have started######################");
                var task = Task.Run(() => {
                    string[] loadingAnim = {
                        "Loading.", "Loading.", "Loading.",
                        "Loading..", "Loading..", "Loading..",
                        "Loading...", "Loading...", "Loading..."
                    };
                    var i = 0;
                    while (!_request.IsDone()) {
                        _text.SetText(loadingAnim[i]);
                        i %= loadingAnim.Length;
                        Main.NewText("Still in task, request" + (_request.IsDone() ? " is done" : " is not done"));
                    }

                    if (_request.Result() == null)
                        ModContent.GetInstance<WikiBrowser>().Logger
                            .Info("#######################Result obtained, but it is null######################");

                    _text.SetText(_request.Result());
                    ModContent.GetInstance<WikiBrowser>().Logger.Info("Task finished, page loaded");
                });
            }
        }
    }
}
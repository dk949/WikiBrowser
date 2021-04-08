using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using WikiBrowser.Requests;
using static WikiBrowser.UI.UiConfig;

namespace WikiBrowser.UI {
    // MainUIState's visibility is toggled by typing "/test" in chat. (See TestCommand.cs)
    internal class MainUIState : UIState {
        public static bool Visible;

        private DragableUIPanel _mainPanel;
        private TerrariaRequest _request;

        private UIHoverImageButton _sendRequestButton;
        private UIText _text;
        private VanillaItemSlotWrapper _vanillaItemSlot;

        public override void OnInitialize() {
            _request = new TerrariaRequest();

            // TODO: this is studpid, move this somewhere else
            _mainPanel = new DragableUIPanel();
            _mainPanel.SetPadding(General.Margin);
            _mainPanel.Left.Set(Panel.InitLeft, 0f);
            _mainPanel.Top.Set(Panel.InitTop, 0f);
            _mainPanel.Width.Set(Panel.Width, 0f);
            _mainPanel.Height.Set(Panel.Height, 0f);
            _mainPanel.BackgroundColor = Panel.Color;


            var closeTexture = ModContent.GetTexture("Terraria/Item_2735"); // this is an X
            var closeButton =
                new UIHoverImageButton(closeTexture,
                    Language.GetTextValue("LegacyInterface.52")); // "close" with localisation
            closeButton.Left.Set(Close.InitLeft, 0f);
            closeButton.Top.Set(Close.InitTop, 0f);
            closeButton.Width.Set(Close.Width, 0f);
            closeButton.Height.Set(Close.Height, 0f);
            closeButton.OnClick += CloseButtonClicked;
            _mainPanel.Append(closeButton);

            var magnifTexture = ModContent.GetTexture("Terraria/Item_216"); // This is a magnifying glass
            _sendRequestButton = new UIHoverImageButton(magnifTexture, "Send request");
            _sendRequestButton.Left.Set(Request.InitLeft, 0f);
            _sendRequestButton.Top.Set(Request.InitTop, 0f);
            _sendRequestButton.Width.Set(Request.Width, 0f);
            _sendRequestButton.Height.Set(Request.Height, 0f);
            _sendRequestButton.OnClick += RequestButtonClicked;
            _mainPanel.Append(_sendRequestButton);


            _vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f);
            _vanillaItemSlot.Left.Set(ItemFrame.InitLeft, 0);
            _vanillaItemSlot.Top.Set(ItemFrame.InitTop, 0);
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
                    while (!_request.IsDone()) {
                        _text.SetText("Loading...");
                        Main.NewText("Sending request");
                    }

                    _text.SetText(_request.Result());
                    ModContent.GetInstance<WikiBrowser>().Logger.Info("Task finished, page loaded");
                });
            }
        }
    }
}
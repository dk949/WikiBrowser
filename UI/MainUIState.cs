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
        private PagedString _body;

        private DragableUIPanel _mainPanel;
        private TerrariaRequest _request;
        private string _title;

        private UIText _uiBody;
        private UIText _uiTitle;
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
            var sendRequestButton = new UIHoverImageButton(magnifTexture, "Search");
            sendRequestButton.Left.Set(Request.InitLeft, 0f);
            sendRequestButton.Top.Set(Request.InitTop, 0f);
            sendRequestButton.Width.Set(Request.Width, 0f);
            sendRequestButton.Height.Set(Request.Height, 0f);
            sendRequestButton.OnClick += RequestButtonClicked;
            _mainPanel.Append(sendRequestButton);

            var upTexture = ModContent.GetTexture(Textures.VoteUp);
            var upButton = new UIHoverImageButton(upTexture, "Page Up");
            upButton.Left.Set(UpButton.InitLeft, 0f);
            upButton.Top.Set(UpButton.InitTop, 0f);
            upButton.Width.Set(UpButton.Width, 0f);
            upButton.Height.Set(UpButton.Height, 0f);
            upButton.OnClick += PageUpClicked;
            _mainPanel.Append(upButton);


            var downTexture = ModContent.GetTexture(Textures.VoteDown);
            var downButton = new UIHoverImageButton(downTexture, "Page Down");
            downButton.Left.Set(DownButton.InitLeft, 0f);
            downButton.Top.Set(DownButton.InitTop, 0f);
            downButton.Width.Set(DownButton.Width, 0f);
            downButton.Height.Set(DownButton.Height, 0f);
            downButton.OnClick += PageDownClicked;
            _mainPanel.Append(downButton);

            _vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f);
            _vanillaItemSlot.Left.Set(ItemFrame.InitLeft, 0);
            _vanillaItemSlot.Top.Set(ItemFrame.InitTop, 0);
            _vanillaItemSlot.ValidItemFunc = item => true;

            _mainPanel.Append(_vanillaItemSlot);

            _title = "";
            _uiTitle = new UIText("");
            _uiTitle.Left.Set(Title.InitLeft, 0);
            _uiTitle.Top.Set(Title.InitTop, 0);
            _uiTitle.Height.Set(Title.Height, 0);
            _uiTitle.Width.Set(Title.Width, 0);
            _mainPanel.Append(_uiTitle);

            _body = new PagedString();
            _uiBody = new UIText("");
            _uiBody.Left.Set(Body.InitLeft, 0);
            _uiBody.Top.Set(Body.InitTop, 0);
            _uiBody.Height.Set(Body.Height, 0);
            _uiBody.Width.Set(Body.Width, 0);
            _mainPanel.Append(_uiBody);

            Append(_mainPanel);
        }


        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuClose);
            if (!_vanillaItemSlot.Item.IsAir) {
                // QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
                Main.LocalPlayer.QuickSpawnClonedItem(_vanillaItemSlot.Item, _vanillaItemSlot.Item.stack);
                _vanillaItemSlot.Item.TurnToAir();
            }

            _uiTitle.SetText("");
            _uiBody.SetText("");
            Visible = false;
        }

        private void RequestButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
            ModContent.GetInstance<WikiBrowser>().Logger.Info("Started request");
            if (_vanillaItemSlot.Item.IsAir) {
                _uiBody.SetText("No Item");
            } else {
                ModContent.GetInstance<WikiBrowser>().Logger.Info("About to call GetItem");
                _request.GetItem(_vanillaItemSlot.Item);
                ModContent.GetInstance<WikiBrowser>().Logger.Info("Successfully called GetItem");
                var task = Task.Run(() => {
                    while (!_request.IsDone()) _uiBody.SetText("Loading...");

                    _title = _request.Result().Title;
                    _uiTitle.SetText(_title);
                    _body.Pages = _request.Result().Body;
                    _uiBody.SetText(_body.GetPage());
                    ModContent.GetInstance<WikiBrowser>().Logger.Info("Task finished, page loaded");
                });
            }
        }

        private void PageUpClicked(UIMouseEvent evt, UIElement listeningElement) {
            _body.CurrentPage = _body.CurrentPage <= 0 ? 0 : _body.CurrentPage - 1;
            _uiBody.SetText(_body.GetPage());
        }

        private void PageDownClicked(UIMouseEvent evt, UIElement listeningElement) {
            _body.CurrentPage = _body.CurrentPage >= _body.Count() - 1 ? _body.Count() - 1 : _body.CurrentPage + 1;
            _uiBody.SetText(_body.GetPage());
        }
    }
}
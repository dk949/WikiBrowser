using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using WikiBrowser.Requests;
using static WikiBrowser.UI.UiConfig;
using static WikiBrowser.Logging;

namespace WikiBrowser.UI {
    // MainUIState's visibility is toggled by typing "/wiki" in chat. (See WikiCommand.cs)
    internal class MainUIState : UIState {
        public static bool Visible;

        private ArticleContainer _article;

        private DragableUIPanel _mainPanel;
        private TerrariaRequest _request;

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
            _mainPanel.BackgroundColor = General.PanelBgColor;


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

            var upTexture = ModContent.GetTexture(Textures.PageUp);
            var upButton = new UIHoverImageButton(upTexture, "");
            upButton.Left.Set(UpButton.InitLeft, 0f);
            upButton.Top.Set(UpButton.InitTop, 0f);
            upButton.Width.Set(UpButton.Width, 0f);
            upButton.Height.Set(UpButton.Height, 0f);
            upButton.OnClick += PageUpClicked;
            _mainPanel.Append(upButton);


            var downTexture = ModContent.GetTexture(Textures.PageDown);
            var downButton = new UIHoverImageButton(downTexture, "");
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

            _article = new ArticleContainer();
            _article.Left.Set(0, 0);
            _article.Top.Set(0, 0);
            _article.Width.Set(Panel.Width, 0);
            _article.Height.Set(Panel.Height, 0);
            _mainPanel.Append(_article);

            Append(_mainPanel);
        }


        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuClose);
            if (!_vanillaItemSlot.Item.IsAir) {
                // QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
                Main.LocalPlayer.QuickSpawnClonedItem(_vanillaItemSlot.Item, _vanillaItemSlot.Item.stack);
                _vanillaItemSlot.Item.TurnToAir();
            }

            //TODO: make this part configurable?
            _article.UiTitle = "";
            _article.UiBody = "";
            Visible = false;
        }

        private void RequestButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
            Log("Started request", LogType.Info);
            if (_vanillaItemSlot.Item.IsAir) {
                _article.UiTitle = "No Item";
                _article.UiBody = "";
                return;
            }

            PerformRequest(_vanillaItemSlot.Item.Name);
        }

        public void PerformRequest(string item) {
            _request.GetItem(item);
            var task = Task.Run(() => {
                while (!_request.IsDone()) {
                    _article.UiBody = "Loading...";
                    _article.UiTitle = "";
                }

                _article.UiTitle = _request.Result().Title;
                _article.UiBody = _request.Result().Body;
                Log("Task finished, page loaded", LogType.Info);
            });
        }

        private void PageUpClicked(UIMouseEvent evt, UIElement listeningElement) {
            _article.PrevPage();
        }

        private void PageDownClicked(UIMouseEvent evt, UIElement listeningElement) {
            _article.NextPage();
        }
    }
}
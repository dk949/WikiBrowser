using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using WikiBrowser.Requests;
using WikiBrowser.UI.SpecialisedUIElements;
using static WikiBrowser.Logging;

namespace WikiBrowser.UI {
    // MainUIState's visibility is toggled by typing "/wiki" in chat. (See WikiCommand.cs)
    internal class MainUiState : UIState {
        public static bool Visible;

        private ArticleContainer _article;

        private DragableUiPanel _mainPanel;

        private Request _tRequest;
        private Request _sRequest;

        private VanillaItemSlotWrapper _itemSlot;

        public override void OnInitialize() {
            _tRequest = new TerrariaRequest();
            _sRequest = new StatRequest();

            _mainPanel = new MainPanel();


            var modeSelector = new UiModeSelector();

            var wikiModeButton = new WikiModeButton(SelectWikiMode);
            modeSelector.Add(wikiModeButton);
            _mainPanel.Append(wikiModeButton);

            var usedInModeButton = new UsedInModeButton(SelectUsedInMode);
            modeSelector.Add(usedInModeButton);
            _mainPanel.Append(usedInModeButton);

            var craftingModeButton = new CraftingModeButton(SelectCraftingMode);
            modeSelector.Add(craftingModeButton);
            _mainPanel.Append(craftingModeButton);

            _mainPanel.Append(modeSelector);


            var closeButton = new CloseButton(CloseButtonClicked);
            _mainPanel.Append(closeButton);


            var searchButton = new SearchButton(SearchButtonClicked);
            _mainPanel.Append(searchButton);


            var upButton = new UpButton(PageUpClicked);
            _mainPanel.Append(upButton);


            var downButton = new DownButton(PageDownClicked);
            _mainPanel.Append(downButton);


            _itemSlot = new SearchItemSlot();
            _mainPanel.Append(_itemSlot);


            _article = new ArticleContainer();
            _mainPanel.Append(_article);

            Append(_mainPanel);
        }

        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuClose);
            ReturnItem();

            //TODO: make this part configurable?
            _article.UiTitle = "";
            _article.UiBody = "";
            _article.UiCurrentPage = 0;
            Visible = false;
        }

        internal void ReturnItem() {
            if (_itemSlot.Item.IsAir) return;
            // QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
            Main.LocalPlayer.QuickSpawnClonedItem(_itemSlot.Item, _itemSlot.Item.stack);
            _itemSlot.Item.TurnToAir();
        }

        private void SearchButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuTick);
            _article.UiCurrentPage = 0;
            Log("Started request", LogType.Info);
            if (_itemSlot.Item.IsAir) {
                _article.UiTitle = "No Item";
                _article.UiBody = "";
                return;
            }

            PerformSRequest(_itemSlot.Item);
        }

        public void PerformRequest(string item) {
            _tRequest.GetItem(item);
            Task.Run(() => {
                while (!_tRequest.IsDone()) {
                    _article.UiBody = "Loading...";
                    _article.UiTitle = "";
                }

                _article.UiTitle = _tRequest.Result().Title;
                _article.UiBody = _tRequest.Result().Body;
                Log("Task finished, page loaded", LogType.Info);
            });
        }

        // TODO: have 2 classes with different UIs. they inherit from one, just change the value fo request
        public void PerformSRequest(Item item) {
            _sRequest.GetItem(item);
            Task.Run(() => {
                while (!_sRequest.IsDone()) {
                    _article.UiBody = "Loading...";
                    _article.UiTitle = "";
                }

                _article.UiTitle = _sRequest.Result().Title;
                _article.UiBody = _sRequest.Result().Body;
                Log("Task finished, page loaded", LogType.Info);
            });
        }

        private void PageUpClicked(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuTick);
            _article.PrevPage();
        }

        private void PageDownClicked(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuTick);
            _article.NextPage();
        }


        private void SelectWikiMode(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuTick);
            Main.NewText("Wiki mode selected");
        }

        private void SelectUsedInMode(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuTick);
            Main.NewText("Used in mode selected");
        }

        private void SelectCraftingMode(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuTick);
            Main.NewText("Crafting mode selected");
        }
    }
}
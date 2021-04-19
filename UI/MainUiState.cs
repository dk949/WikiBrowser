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

        private UiModeSelector _modeSelector;

        private WikiRequest _wikiRequest;
        private CraftingRequest _craftingRequest;
        private UsedInRequest _usedInRequest;

        private readonly Result[] _results = {new Result(), new Result(), new Result()};

        private VanillaItemSlotWrapper _itemSlot;

        public override void OnInitialize() {
            _wikiRequest = new WikiRequest();
            _craftingRequest = new CraftingRequest();
            _usedInRequest = new UsedInRequest();

            _mainPanel = new MainPanel();


            _modeSelector = new UiModeSelector();

            var wikiModeButton = new WikiModeButton(SelectWikiMode);
            _modeSelector.Add(wikiModeButton);
            _mainPanel.Append(wikiModeButton);

            var usedInModeButton = new UsedInModeButton(SelectUsedInMode);
            _modeSelector.Add(usedInModeButton);
            _mainPanel.Append(usedInModeButton);

            var craftingModeButton = new CraftingModeButton(SelectCraftingMode);
            _modeSelector.Add(craftingModeButton);
            _mainPanel.Append(craftingModeButton);

            _mainPanel.Append(_modeSelector);


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
            ResetArticle();
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
            //ResetArticle();
            _article.UiCurrentPage = 0;
            Log("Started request", LogType.Info);
            if (_itemSlot.Item.IsAir) {
                WriteToAll("No Item", "");
                return;
            }

            PerformItemRequest(_itemSlot.Item);
        }

        public void PerformRequest(string item) {
            _wikiRequest.GetItem(item);
            Task.Run(() => {
                while (!_wikiRequest.IsDone()) {
                    WriteToAll("", "Loading...");
                }

                _results[0] = _wikiRequest.Result();
                _results[1] = Helpers.ResultUnavailable;
                _results[2] = Helpers.ResultUnavailable;

                _article.UiTitle = _results[0].Title;
                _article.UiBody = _results[0].Body;
                Log("Task finished, page loaded", LogType.Info);
            });
        }

        public void PerformItemRequest(Item item) {
            _wikiRequest.GetItem(item);
            _usedInRequest.GetItem(item);
            _craftingRequest.GetItem(item);
            Task.Run(() => {
                while (!(_wikiRequest.IsDone() && _usedInRequest.IsDone() && _craftingRequest.IsDone())) {
                    WriteToAll("", "Loading...");
                }

                PopulateArticle();
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
            SwitchToWindow(0);
        }


        private void SelectUsedInMode(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuTick);
            SwitchToWindow(1);
        }

        private void SelectCraftingMode(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuTick);
            SwitchToWindow(2);
        }

        private void ResetArticle() {
            _article.UiTitle = "";
            _article.UiBody = "\n";
            for (var i = 0; i < _results.Length; i++) {
                _results[i].Title = "";
                _results[i].Body = "\n";
            }

            _modeSelector.Reset();
            _article.UiCurrentPage = 0;
        }

        private void WriteToAll(string title, string body) {
            if (body == "") body = "\n";

            for (var i = 0; i < _results.Length; i++) {
                _results[i].Body = body;
                _results[i].Title = title;
            }

            _article.UiBody = _results[0].Body;
            _article.UiTitle = _results[0].Title;
        }

        private void PopulateArticle() {
            _results[0] = _wikiRequest.Result();
            _results[1] = _usedInRequest.Result();
            _results[2] = _craftingRequest.Result();

            _article.UiTitle = _results[_modeSelector.CurrentlySelected].Title;
            _article.UiBody = _results[_modeSelector.CurrentlySelected].Body;
        }

        private void SwitchToWindow(int window) {
            if (_results[window].Title != null) _article.UiTitle = _results[window].Title;
            if (_results[window].Body != null) _article.UiBody = _results[window].Body;
            _article.UiCurrentPage = 0;
        }
    }
}
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static WikiBrowser.UI.UiConfig;

namespace WikiBrowser.UI.SpecialisedUIElements {
    internal class ArticleContainer : UIElement {
        private PagedString _body;
        private string _title;

        private UIText _uiBody;
        private UIText _uiTitle;

        public string UiTitle {
            get => _uiTitle?.Text ?? string.Empty;
            set => _title = value;
        }

        public string UiBody {
            get => _uiBody?.Text ?? string.Empty;
            set => _body.Pages = value;
        }

        public int UiCurrentPage {
            get => _body.CurrentPage;
            set => _body.CurrentPage = value;
        }

        public ArticleContainer() {
            Left.Set(0, 0);
            Top.Set(0, 0);
            Width.Set(Panel.Width, 0);
            Height.Set(Panel.Height, 0);
        }

        public override void OnInitialize() {
            base.OnInitialize(); // Not sure if required, will leave here for now
            _title = "";
            _uiTitle = new UIText("");
            _uiTitle.Left.Set(Title.InitLeft, 0);
            _uiTitle.Top.Set(Title.InitTop, 0);
            _uiTitle.Height.Set(Title.Height, 0);
            _uiTitle.Width.Set(Title.Width, 0);
            Append(_uiTitle);

            _body = new PagedString();
            _uiBody = new UIText("");
            _uiBody.Left.Set(Body.InitLeft, 0);
            _uiBody.Top.Set(Body.InitTop, 0);
            _uiBody.Height.Set(Body.Height, 0);
            _uiBody.Width.Set(Body.Width, 0);
            Append(_uiBody);
        }

        public override void Update(GameTime gameTime) {
            if (_title == null) return;
            _uiTitle.SetText(_title);
            _title = null;
            _uiBody.SetText(_body.GetPage());
            Recalculate();
            MinWidth = _uiTitle.MinWidth;
            MinHeight = _uiTitle.MinHeight;
        }

        public void PrevPage() {
            _body.CurrentPage = _body.CurrentPage <= 0 ? 0 : _body.CurrentPage - 1;
            _title = _uiTitle.Text; // Sets a flag that the text needs to be redrawn
        }

        public void NextPage() {
            _body.CurrentPage = _body.CurrentPage >= _body.Count() - 1 ? _body.Count() - 1 : UiCurrentPage + 1;
            _title = _uiTitle.Text; // Sets a flag that the text needs to be redrawn
        }
    }
}
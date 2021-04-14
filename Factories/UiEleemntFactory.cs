using Terraria.UI;

namespace WikiBrowser.Factories {
    public class UiEleemntFactory {
        public static T New<T>() where T : UIElement, new() {
            var instance = new T();

            return instance;
        }
    }
}
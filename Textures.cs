namespace WikiBrowser {
    internal static class Textures {
        internal static readonly string PageUp = TextureHelper.AddPath("PageUp");
        internal static readonly string PageDown = TextureHelper.AddPath("PageDown");
    }

    internal static class TextureHelper {
        public static string AddPath(string textureName) {
            return "WikiBrowser/Textures/" + textureName;
        }
    }
}
using Microsoft.Xna.Framework;

namespace WikiBrowser.UI {
    public static class UiConfig {
        public struct General {
            public const float IconWidth = 40;
            public const float IconHeight = 40;
            public const float IconGaps = 15;
            public const float Margin = 30;
        }

        public struct Panel {
            public const float InitLeft = 600;
            public const float InitTop = 100;
            public const float Width = 720;
            public const float Height = 600;

            public static readonly Color Color = new Color(73, 94, 171, 200);
        }

        public struct Close {
            public const float InitLeft = Panel.Width - Width - General.Margin;
            public const float InitTop = 0;
            public const float Width = General.IconWidth;
            public const float Height = General.IconHeight;
        }

        public struct Request {
            public const float InitLeft = Panel.Width - Width - General.Margin;
            public const float InitTop = Close.Height + General.IconGaps;
            public const float Width = General.IconWidth;
            public const float Height = General.IconHeight;
        }


        public struct ItemFrame {
            public const float InitLeft = 0;
            public const float InitTop = 0;
        }
    }
}
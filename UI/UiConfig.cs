using Microsoft.Xna.Framework;

namespace WikiBrowser.UI {
    public static class UiConfig {
        public static readonly UiElementConfig Panel = new UiElementConfig {
            InitLeft = 600,
            InitTop = 100,
            Width = 620,
            Height = 600
        };

        public static readonly UiElementConfig Close = new UiElementConfig {
            InitLeft = Panel.Width - General.IconWidth - General.Margin,
            InitTop = 0,
            Width = General.IconWidth,
            Height = General.IconHeight
        };

        public static readonly UiElementConfig Request = new UiElementConfig {
            InitLeft = Panel.Width - General.IconWidth - General.Margin,
            InitTop = Close.Height + General.IconGaps,
            Width = General.IconWidth,
            Height = General.IconHeight
        };

        public static readonly UiElementConfig UpButton = new UiElementConfig {
            InitLeft = 0,
            InitTop = 60,
            Width = General.IconWidth,
            Height = General.IconHeight
        };

        public static readonly UiElementConfig DownButton = new UiElementConfig {
            InitLeft = 0,
            InitTop = UpButton.InitTop + General.Margin,
            Width = General.IconWidth,
            Height = General.IconHeight
        };

        public static readonly UiElementConfig ItemFrame = new UiElementConfig {
            InitLeft = 0,
            InitTop = 0,
            Width = 0, // Item frames' width and height are constant, these fields have no meaning
            Height = 0
        };

        public static readonly UiElementConfig Body = new UiElementConfig {
            InitLeft = General.IconWidth + General.IconGaps,
            InitTop = 60,
            Width = 70,
            Height = 500
        };

        public static readonly UiElementConfig Title = new UiElementConfig {
            InitLeft = 0,
            InitTop = 0,
            Width = Panel.Width,
            Height = 10
        };

        public static readonly UiElementConfig WikiMode = new UiElementConfig {
            InitLeft = Panel.Width / 2 - 3 * (General.IconWidth + General.IconGaps) / 2 + General.IconGaps / 2,
            InitTop = Panel.Height - General.IconHeight - General.Margin,
            Width = General.IconWidth,
            Height = General.IconHeight
        };

        public static readonly UiElementConfig UsedInMode = new UiElementConfig {
            InitLeft = WikiMode.InitLeft + General.IconWidth / 2 + General.IconGaps,
            InitTop = WikiMode.InitTop,
            Width = General.IconWidth,
            Height = General.IconHeight
        };

        public static readonly UiElementConfig CraftingMode = new UiElementConfig {
            InitLeft = UsedInMode.InitLeft + General.IconWidth / 2 + General.IconGaps,
            InitTop = UsedInMode.InitTop,
            Width = General.IconWidth,
            Height = General.IconHeight
        };

        public struct General {
            public const float IconWidth = 40;
            public const float IconHeight = 40;
            public const float IconGaps = 15;
            public const float Margin = 30;
            public static readonly Color PanelBgColor = new Color(73, 94, 171, 200);
        }

        public struct UiElementConfig {
            public float InitLeft;
            public float InitTop;
            public float Width;
            public float Height;
        }
    }
}
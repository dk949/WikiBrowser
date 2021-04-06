namespace WikiBrowser {
    // This may look horrifying, and it is.
     
    public static class Textures {
        internal static readonly string NPCDamageIcon = TextureHelper.AddPath("NPCDamageIcon");
        internal static readonly string NPCDefenseIcon = TextureHelper.AddPath("NPCDefenseIcon");
        internal static readonly string NPCKnockbackIcon = TextureHelper.AddPath("NPCKnockbackIcon");
        internal static readonly string NPCLifeIcon = TextureHelper.AddPath("NPCLifeIcon");
        internal static readonly string Snap = TextureHelper.AddPath("Snap");
        internal static readonly string VoteDown = TextureHelper.AddPath("VoteDown");
        internal static readonly string VoteUp = TextureHelper.AddPath("VoteUp");
        internal static readonly string checkBox = TextureHelper.AddPath("checkBox");
        internal static readonly string checkMark = TextureHelper.AddPath("checkMark");
        internal static readonly string closeButton = TextureHelper.AddPath("closeButton");

        public static class Images {
            internal static readonly string bVacuum = TextureHelper.AddPath("Images.bVacuum");
            internal static readonly string canIcon = TextureHelper.AddPath("Images.canIcon");
            internal static readonly string connectedPlayers = TextureHelper.AddPath("Images.connectedPlayers");
            internal static readonly string login = TextureHelper.AddPath("Images.login");
            internal static readonly string logout = TextureHelper.AddPath("Images.logout");
            internal static readonly string manageGroups = TextureHelper.AddPath("Images.manageGroups");
            internal static readonly string moonIcon = TextureHelper.AddPath("Images.moonIcon");
            internal static readonly string npcIcon = TextureHelper.AddPath("Images.npcIcon");
            internal static readonly string rainIcon = TextureHelper.AddPath("Images.rainIcon");
            internal static readonly string speed0 = TextureHelper.AddPath("Images.speed0");
            internal static readonly string speed1 = TextureHelper.AddPath("Images.speed1");
            internal static readonly string speed2 = TextureHelper.AddPath("Images.speed2");
            internal static readonly string speed3 = TextureHelper.AddPath("Images.speed3");
            internal static readonly string speed4 = TextureHelper.AddPath("Images.speed4");
            internal static readonly string sunIcon = TextureHelper.AddPath("Images.sunIcon");
            internal static readonly string waypointIcon = TextureHelper.AddPath("Images.waypointIcon");

            public static class CTF {
                internal static readonly string blueFlag = TextureHelper.AddPath("Images.CTF.blueFlag");
                internal static readonly string redFlag = TextureHelper.AddPath("Images.CTF.redFlag");
            }

            public static class CollapseBar {
                internal static readonly string CollapseArrowHorizontal =
                    TextureHelper.AddPath("Images.CollapseBar.CollapseArrowHorizontal");

                internal static readonly string CollapseButtonHorizontal =
                    TextureHelper.AddPath("Images.CollapseBar.CollapseButtonHorizontal");
            }

            public static class UIKit {
                internal static readonly string barEdge = TextureHelper.AddPath("Images.UIKit.barEdge");
                internal static readonly string buttonEdge = TextureHelper.AddPath("Images.UIKit.buttonEdge");
                internal static readonly string checkBox = TextureHelper.AddPath("Images.UIKit.checkBox");
                internal static readonly string checkMark = TextureHelper.AddPath("Images.UIKit.checkMark");
                internal static readonly string clearIcon = TextureHelper.AddPath("Images.UIKit.clearIcon");
                internal static readonly string dropdownCapDown = TextureHelper.AddPath("Images.UIKit.dropdownCapDown");
                internal static readonly string dropdownCapUp = TextureHelper.AddPath("Images.UIKit.dropdownCapUp");
                internal static readonly string openIcon = TextureHelper.AddPath("Images.UIKit.openIcon");
                internal static readonly string saveIcon = TextureHelper.AddPath("Images.UIKit.saveIcon");
                internal static readonly string scrollbarEdge = TextureHelper.AddPath("Images.UIKit.scrollbarEdge");
                internal static readonly string scrollbgEdge = TextureHelper.AddPath("Images.UIKit.scrollbgEdge");
                internal static readonly string textboxEdge = TextureHelper.AddPath("Images.UIKit.textboxEdge");
            }
        }
    }

    static class TextureHelper {
        public static string AddPath(string textureName) {
            return "WikiBrowser/Textures/" + textureName;
        }
    }
}
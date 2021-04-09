using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using WikiBrowser.UI;

namespace WikiBrowser {
    public class WikiBrowser : Mod {
        public static ModHotKey RandomBuffHotKey;
        // With the new fonts in 1.3.5, font files are pretty big now so you need to generate the font file before building the mod.
        // You can use https://forums.terraria.org/index.php?threads/dynamicspritefontgenerator-0-4-generate-fonts-without-xna-game-studio.57127/ to make dynamicspritefonts

        private UserInterface _panelInterface;

        internal MainUIState UiState;

        // Your mod instance has a Logger field, use it.
        // OPTIONAL: You can create your own logger this way, recommended is a custom logging class if you do a lot of logging
        // You need to reference the log4net library to do this, this can be found in the tModLoader repository
        // inside the references folder. You do not have to add this to build.txt as tML has it natively.
        // internal ILog Logging = LogManager.GetLogger("ExampleMod");

        public override void Load() {
            // Will show up in client.log
            Logger.InfoFormat("{0} ogging", Name);
            // In older tModLoader versions we used: ErrorLogger.Log("blabla");
            // Replace that with above

            // Registers a new hotkey
            //RandomBuffHotKey = RegisterHotKey("Random Buff", "P"); // See https://docs.microsoft.com/en-us/previous-versions/windows/xna/bb197781(v=xnagamestudio.41) for special keys

            // Registers a new custom currency

            // All code below runs only if we're not loading on a server
            if (!Main.dedServ) {
                // Custom UI
                UiState = new MainUIState();
                UiState.Activate();
                _panelInterface = new UserInterface();
                _panelInterface.SetState(UiState);


                // UserInterface can only show 1 UIState at a time. If you want different "pages" for a UI, switch between UIStates on the same UserInterface instance. 
                // We want both the Coin counter and the Example Person UI to be independent and coexist simultaneously, so we have them each in their own UserInterface.
                // We will call .SetState later in ExamplePerson.OnChatButtonClicked
            }
        }

        public override void Unload() {
            // All code below runs only if we're not loading on a server
            if (!Main.dedServ) {
                //
            }

            // Unload static references
            // You need to clear static references to assets (Texture2D, SoundEffects, Effects). 
            // In addition to that, if you want your mod to completely unload during unload, you need to clear static references to anything referencing your Mod class
            // RandomBuffHotKey = null;
        }


        public override void UpdateUI(GameTime gameTime) {
            if (MainUIState.Visible) _panelInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            var mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Body"));
            if (mouseTextIndex != -1)
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ExampleMod: Coins Per Minute",
                    delegate {
                        if (MainUIState.Visible) _panelInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
        }
    }
}
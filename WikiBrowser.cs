using WikiBrowser.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace WikiBrowser {
    public class WikiBrowser : Mod {
        public static ModHotKey RandomBuffHotKey;

        private UserInterface _panelInterface;

        internal MainUIState UiState;

        public WikiBrowser() {
            //Properties = new ModProperties()
            //{
            //	Autoload = true,
            //	AutoloadGores = true,
            //	AutoloadSounds = true,
            //	AutoloadBackgrounds = true
            //};
        }

        public override void Load() {
            Logger.InfoFormat("{0} Logging", Name);
            // All code below runs only if we're not loading on a server
            if (!Main.dedServ) {
                // Custom UI
                UiState = new MainUIState();
                UiState.Activate();
                _panelInterface = new UserInterface();
                _panelInterface.SetState(UiState);
            }
        }

        public override void Unload() {
            // All code below runs only if we're not loading on a server
            if (!Main.dedServ) {
                //
            }
        }


        public override void UpdateUI(GameTime gameTime) {
            if (MainUIState.Visible) {
                _panelInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            var mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1) {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ExampleMod: Coins Per Minute",
                    delegate {
                        if (MainUIState.Visible) {
                            _panelInterface.Draw(Main.spriteBatch, new GameTime());
                        }

                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
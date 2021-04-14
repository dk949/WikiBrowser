using Terraria.UI;
using static WikiBrowser.UI.UiConfig;

namespace WikiBrowser.UI.SpecialisedUIElements {
    internal class SearchItemSlot : VanillaItemSlotWrapper {
        public SearchItemSlot() : base(ItemSlot.Context.BankItem, 0.85f) {
            Left.Set(ItemFrame.InitLeft, 0);
            Top.Set(ItemFrame.InitTop, 0);
            ValidItemFunc = item => true;
        }
    }
}
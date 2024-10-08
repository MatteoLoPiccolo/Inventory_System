using InventorySystemController;
using System;

namespace InventorySystemUI
{
    public class PurchasePopupUI : ItemPopupUI
    {
        public event Action<int> OnPurchaseConfirmed;

        protected override void OnConfirmAction()
        {
            OnPurchaseConfirmed?.Invoke(itemIndex);
        }

        public override void ClosePopup()
        {
            base.ClosePopup();
            OnPurchaseConfirmed -= GameManager.Instance.ShopController.OnPurchaseConfirmed;
        }
    }
}
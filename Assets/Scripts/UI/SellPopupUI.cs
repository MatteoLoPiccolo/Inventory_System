using InventorySystemController;
using System;

namespace InventorySystemUI
{
    public class SellPopupUI : ItemPopupUI
    {
        public event Action<int> OnSellConfirmed;

        protected override void OnConfirmAction()
        {
            OnSellConfirmed?.Invoke(itemIndex);
        }

        public override void ClosePopup()
        {
            base.ClosePopup();
            OnSellConfirmed -= GameManager.Instance.InventoryController.OnSellConfirmed;
        }
    }
}
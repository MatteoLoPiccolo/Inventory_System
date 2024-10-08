using InventorySystemModel;
using InventorySystemPlayer;
using InventorySystemUI;
using UnityEngine;

namespace InventorySystemController
{
    public class ShopController : BaseInventoryController
    {
        private ShopUI shopUI;
        private ShopSO shopData;
        private UIController uiController;
        private PlayerInventory playerInventory;
        private int shopItemsListSize;

        public ShopController(ShopUI shopUI, ShopSO shopData, UIController uiManager, PlayerInventory playerInventory, int shopItemsListSize = 7)
        {
            this.shopUI = shopUI;
            this.shopData = shopData;
            uiController = uiManager;
            this.playerInventory = playerInventory;
            this.shopItemsListSize = shopItemsListSize;

            InitializeShopController();
        }

        private void InitializeShopController()
        {
            shopData.Initialize(shopItemsListSize);
            SetUpUI();
        }

        protected override void SetUpUI()
        {
            shopUI.InitializeUIList(shopItemsListSize);
            shopUI.OnDescriptionRequested += OnDescriptionRequested;

            foreach (var item in shopData.GetCurrentShopItemState())
            {
                shopUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
            }
        }

        protected override void OnDescriptionRequested(int itemIndex)
        {
            Items shopItem = shopData.GetItemAt(itemIndex);

            if (shopItem.IsEmpty)
                return;

            ItemSO item = shopItem.Item;
            shopUI.UpdateShopItemDescription(itemIndex, item.ItemImage, item.ItemName, item.Description, item.Price);
        }

        public void OnPurchaseConfirmed(int itemIndex)
        {
            Items shopItem = shopData.GetItemAt(itemIndex);

            if (shopItem.IsEmpty)
            {
                Debug.Log("Shop item is empty, cannot proceed with purchase.");
                return;
            }

            if (playerInventory.CanAfford(shopItem.Item.Price))
            {
                int buyQuantity = 1;
                playerInventory.AddItem(shopItem.Item, buyQuantity);

                int newPlayerMoney = playerInventory.GetMoney() - shopItem.Item.Price;
                int newShopMoney = GameManager.Instance.ShopController.GetMoney() + shopItem.Item.Price;

                playerInventory.SetMoney(newPlayerMoney);
                GameManager.Instance.ShopController.SetMoney(newShopMoney);

                shopItem.OnQuantityChanged += (newQuantity) =>
                {
                    shopUI.UpdateData(itemIndex, shopItem.Item.ItemImage, newQuantity);
                };

                if (shopItem.Quantity > 0)
                {
                    shopItem.ChangeQuantity();
                }
            }
            else
            {
                Debug.Log("Not enough money!");
            }
        }
    }
}
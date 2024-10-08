using InventorySystemModel;
using InventorySystemPlayer;
using System;
using InventorySystemUI;
using UnityEngine;

namespace InventorySystemController
{
    public class ShopController
    {
        private ShopUI shopUI;
        private ShopSO shopData;
        private UIController uiController;
        private PlayerInventory playerInventory;
        private int shopItemsListSize;
        private int shopMoney;

        public ShopUI GetShopUI() => shopUI;

        public int GetMoney() => shopMoney;

        public void SetMoney(int amount)
        {
            shopMoney = amount;
            OnMoneyChanged?.Invoke(shopMoney);
        }

        public event Action<int> OnMoneyChanged;

        public ShopController(ShopUI shopUI, ShopSO shopData, UIController uiManager, PlayerInventory playerInventory, int shopItemsListSize = 7, int initialMoney = 1000)
        {
            this.shopUI = shopUI;
            this.shopData = shopData;
            uiController = uiManager;
            this.playerInventory = playerInventory;
            this.shopItemsListSize = shopItemsListSize;
            shopMoney = initialMoney;

            InitializeShopController();
        }

        private void InitializeShopController()
        {
            shopData.Initialize(shopItemsListSize);
            SetUpUI();
        }

        private void SetUpUI()
        {
            shopUI.InitializeUIList(shopItemsListSize);
            shopUI.OnDescriptionRequested += OnShopDescriptionRequested;

            foreach (var item in shopData.GetCurrentShopItemState())
            {
                shopUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
            }
        }

        private void OnShopDescriptionRequested(int itemIndex)
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
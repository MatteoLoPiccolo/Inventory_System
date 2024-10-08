using InventorySystemModel;
using InventorySystemPlayer;
using System;
using System.Linq;
using InventorySystemUI;
using UnityEngine;

namespace InventorySystemController
{
    public class InventoryController
    {
        private PlayerInventoryUI playerInventoryUI;
        private PlayerInventory playerInventory;
        private ShopSO shopData;
        private ShopUI shopUI;
        private int inventoryItemsListSize;
        private int inventoryMoney;

        public event Action<int> OnMoneyChanged;

        public PlayerInventoryUI GetInventoryUI() => playerInventoryUI;

        public int ShopMoney
        {
            get => inventoryMoney;

            private set
            {
                inventoryMoney = value;
                OnMoneyChanged?.Invoke(inventoryMoney);
            }
        }

        public InventoryController(PlayerInventoryUI playerInventoryUI, PlayerInventory playerInventory, ShopSO shopData, ShopUI shopUI, int initialMoney = 100)
        {
            this.playerInventoryUI = playerInventoryUI;
            this.playerInventory = playerInventory;
            this.shopData = shopData;
            this.shopUI = shopUI;
            inventoryMoney = initialMoney;

            InitializeInventoryController();
        }

        private void InitializeInventoryController()
        {
            shopData.Initialize(inventoryItemsListSize);
            SetUpUI();
        }

        private void SetUpUI()
        {
            playerInventoryUI.OnDescriptionRequested += OnInventoryDescriptionRequested;

            var inventoryItems = playerInventory.GetInventoryItems().ToList();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                var item = inventoryItems[i];
                ItemSO itemSO = item.Key;
                int quantity = item.Value;

                playerInventoryUI.UpdateData(i, itemSO, quantity);
            }
        }

        private void OnInventoryDescriptionRequested(int itemIndex)
        {
            var inventoryItems = playerInventory.GetInventoryItems().ToList();

            if (itemIndex < 0 || itemIndex >= inventoryItems.Count)
            {
                Debug.LogWarning("Invalid item index.");
                return;
            }

            var itemEntry = inventoryItems[itemIndex];
            ItemSO itemSO = itemEntry.Key;
            int quantity = itemEntry.Value;

            if (itemSO == null)
                return;

            playerInventoryUI.UpdateInventoryItemDescription(itemIndex, itemSO.ItemImage, itemSO.ItemName, itemSO.Description, itemSO.Price);
        }

        public void OnSellConfirmed(int itemIndex)
        {
            var playerItems = playerInventory.GetInventoryItems();
            var playerItem = playerItems.ElementAtOrDefault(itemIndex);

            if (playerItem.Key == null || playerItem.Value <= 0)
            {
                Debug.Log("InventorySystemPlayer item is empty, cannot proceed with sale.");
                return;
            }

            int sellQuantity = 1;

            playerInventory.RemoveItem(playerItem.Key, sellQuantity);
            shopUI.AddItemToShopUI(playerItem.Key, sellQuantity);

            int newPlayerMoney = playerInventory.GetMoney() + playerItem.Key.Price;
            playerInventory.SetMoney(newPlayerMoney);

            int newShopMoney = GameManager.Instance.ShopController.GetMoney() - playerItem.Key.Price;
            GameManager.Instance.ShopController.SetMoney(newShopMoney);

            playerInventoryUI.UpdateData(itemIndex, playerItem.Key, playerItem.Value - sellQuantity);
        }
    }
}
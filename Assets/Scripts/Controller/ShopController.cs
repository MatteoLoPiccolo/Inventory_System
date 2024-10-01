using System;
using UnityEngine;

public class ShopController
{
    private ShopUI shopUI;
    private ShopSO shopData;
    private UIController uiController;
    private PlayerInventory playerInventory;

    private int shopItemsListSize = 7;
    private int shopMoney = 1000;

    public event Action<int> OnMoneyChanged;

    public int ShopMoney
    {
        get => shopMoney;

        private set
        {
            shopMoney = value;
            OnMoneyChanged?.Invoke(shopMoney);
        }
    }

    public ShopController(ShopUI shopUI, ShopSO shopData, UIController uiManager, PlayerInventory playerInventory, int shopItemsListSize = 7, int initialMoney = 1000)
    {
        this.shopUI = shopUI;
        this.shopData = shopData;
        this.uiController = uiManager;
        this.playerInventory = playerInventory;
        this.shopItemsListSize = shopItemsListSize;
        this.shopMoney = initialMoney;

        InitializeShopController();
    }

    private void InitializeShopController()
    {
        shopData.Initialize(shopItemsListSize);
        SetUpUI();

        Debug.Log("OnPurchaseConfirmed event subscribed.");
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
        Debug.Log("OnShopDescriptionRequested(int itemIndex) called");

        Items shopItem = shopData.GetItemAt(itemIndex);

        if (shopItem.IsEmpty)
            return;

        ItemSO item = shopItem.Item;
        shopUI.UpdateShopItemDescription(itemIndex, item.ItemImage, item.ItemName, item.Description, item.Price);
    }

    public void OnPurchaseConfirmed(int itemIndex)
    {
        Debug.Log("OnConfirmPurchase called");

        Items shopItem = shopData.GetItemAt(itemIndex);
        Debug.Log($"Shop item retrieved: {shopItem.Item.ItemName}");

        if (shopItem.IsEmpty)
        {
            Debug.Log("Shop item is empty, cannot proceed with purchase.");
            return;
        }

        if (playerInventory.CanAfford(shopItem.Item.Price))
        {
            Debug.Log($"Player can afford {shopItem.Item.ItemName}. Price: {shopItem.Item.Price}");

            GameManager.Instance.InventoryController.AddItemToInventory(shopItem.Item);

            int newPlayerMoney = playerInventory.GetMoney() - shopItem.Item.Price;
            int newShopMoney = GameManager.Instance.ShopController.ShopMoney + shopItem.Item.Price;

            playerInventory.SetMoney(newPlayerMoney);
            GameManager.Instance.ShopController.SetMoney(newShopMoney);

            shopUI.UpdateData(itemIndex, shopItem.Item.ItemImage, shopItem.Quantity - 1);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void SetMoney(int amount)
    {
        if (ShopMoney >= amount)
        {
            ShopMoney -= amount;
        }
    }

    public ShopUI GetShopUI() => shopUI;
}
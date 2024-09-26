using System;
using UnityEngine;

public class ShopController
{
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private ShopSO shopData;
    [SerializeField] private UIController uiManager;
    [SerializeField] private int shopItemsListSize = 7;
    [SerializeField] private int shopMoney = 1000;

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

    public ShopController(ShopUI shopUI, ShopSO shopData, UIController uiManager, int shopItemsListSize = 7, int initialMoney = 1000)
    {
        this.shopUI = shopUI;
        this.shopData = shopData;
        this.uiManager = uiManager;
        this.shopItemsListSize = shopItemsListSize;
        this.shopMoney = initialMoney;

        InitializeShopController();
    }

    private void InitializeShopController()
    {
        shopData.Initialize(shopItemsListSize);
        SetUpUI();
    }

    private void SetUpUI()
    {
        shopUI.InitializeShopUIList(shopItemsListSize);
        shopUI.OnDescriptionRequested += OnDescriptionRequested;

        foreach (var item in shopData.GetCurrentShopItemState())
        {
            shopUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
        }
    }

    private void OnDescriptionRequested(int itemIndex)
    {
        Items shopItem = shopData.GetItemAt(itemIndex);

        if(shopItem.IsEmpty)
            return;

        ItemSO item = shopItem.Item;
        shopUI.UpdateDescription(itemIndex, item.ItemImage, item.ItemName, item.Description, item.Price);
    }

    public int GetMoney() => ShopMoney;

    public void SetMoney(int newAmount) => ShopMoney = newAmount;
}
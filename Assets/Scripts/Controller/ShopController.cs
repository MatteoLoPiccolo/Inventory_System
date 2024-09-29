using System;

public class ShopController
{
    private ShopUI shopUI;
    private ShopSO shopData;
    private UIController uiManager;

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

        if (shopItem.IsEmpty)
            return;

        ItemSO item = shopItem.Item;
        shopUI.UpdateDescription(itemIndex, item.ItemImage, item.ItemName, item.Description, item.Price);
    }
    public void SetMoney(int amount)
    {
        if (ShopMoney >= amount)
        {
            ShopMoney -= amount;
        }
    }

    public int GetShopItemsListSize() => shopItemsListSize;

    public ShopUI GetShopUI() => shopUI;
}
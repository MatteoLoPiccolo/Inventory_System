using System;

public class InventoryController
{
    private PlayerInventoryUI playerInventoryUI;
    private PlayerInventory playerInventory;
    private ShopSO shopData;

    private int inventoryItemsListSize;
    private int inventoryMoney = 100;

    public event Action<int> OnMoneyChanged;

    public int ShopMoney
    {
        get => inventoryMoney;

        private set
        {
            inventoryMoney = value;
            OnMoneyChanged?.Invoke(inventoryMoney);
        }
    }

    public InventoryController(PlayerInventoryUI playerInventoryUI, PlayerInventory playerInventory, ShopSO shopData, int initialMoney = 100)
    {
        this.playerInventoryUI = playerInventoryUI;
        this.playerInventory = playerInventory;
        this.shopData = shopData;
        this.inventoryMoney = initialMoney;

        InitializeInventoryController();
    }

    private void InitializeInventoryController()
    {
        shopData.Initialize(inventoryItemsListSize);
        SetUpUI();
    }

    private void SetUpUI()
    {
        playerInventoryUI.InitializeList(inventoryItemsListSize);
        playerInventoryUI.OnDescriptionRequested += OnDescriptionRequested;

        foreach (var item in shopData.GetCurrentShopItemState())
        {
            playerInventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
        }
    }

    private void OnDescriptionRequested(int itemIndex)
    {
        Items shopItem = shopData.GetItemAt(itemIndex);

        if (shopItem.IsEmpty)
            return;

        ItemSO item = shopItem.Item;
        playerInventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.ItemName, item.Description, item.Price);
    }

    public void SetMoney(int amount)
    {
        if (ShopMoney >= amount)
        {
            ShopMoney -= amount;
        }
    }

    public void AddItemToInventory(ItemSO item)
    {
        playerInventoryUI.AddItemUI(item, 1);  
        playerInventory.AddItem(item, 1);
    }

    public PlayerInventoryUI GetInventoryUI() => playerInventoryUI;
}
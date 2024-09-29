using System;
using UnityEngine;

public class ShopUI : BaseInventoryUI<ShopItemUI>
{
    [SerializeField] private ShopItemUI itemPrefab;
    [SerializeField] private PurchasePopupUI purchasePopupPrefab;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private ShopSO shopItemSO;

    public event Action<int> OnItemLeftClicked;
    public event Action<int> OnItemRightClicked;

    protected override void Awake()
    {
        base.Awake();
        itemDescription.ResetDescription();
        purchasePopupPrefab.gameObject.SetActive(false);
    }

    // Initialize the list of items in the shop with a list
    protected override void InitializeUIList(int listSize)
    {
        for (int i = 0; i < listSize; i++)
        {
            ShopItemUI uiShopItem = Instantiate(itemPrefab, contentRectTransform);
            uiItems.Add(uiShopItem);

            uiShopItem.OnItemClicked += OnLeftClick;
            uiShopItem.OnRightMouseButtonClicked += OnRightClick;
        }
    }

    public void InitializeShopUIList(int listSize)
    {
        InitializeUIList(listSize);
    }

    public new void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description, int price)
    {
        base.UpdateDescription(itemIndex, itemImage, name, description, price);
    }

    // Update the data with image and quantity
    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (itemIndex < uiItems.Count)
            uiItems[itemIndex].SetItem(itemImage, itemQuantity);
    }

    // Event called when clicked on item in the UI
    //protected override void OnLeftClick((ShopItemUI itemUI)
    //{
    //    int index = uiItems.IndexOf(uIShopItem);
    //    if (index == -1)
    //        return;

    //    Items shopItem = shopItemSO.GetItemAt(index);
    //    if (shopItem == null || shopItem.Item == null)
    //        return;

    //    OnDescriptionRequested?.Invoke(index);

    //    if (activePopupInstance)
    //        activePopupInstance.Show(shopItem.Item.ItemImage, shopItem.Item.ItemName, shopItem.Quantity, shopItem.Item.Price, index, shopItemSO, playerInventory);
    //}

    //public void UiShopItem_OnRightMouseButtonClicked(ShopItemUI uIShopItem)
    //{
    //    if (activePopupInstance)
    //    {
    //        int index = uiItems.IndexOf(uIShopItem);
    //        if (index == -1)
    //            return;

    //        Items shopItem = shopItemSO.GetItemAt(index);
    //        if (shopItem == null || shopItem.Item == null)
    //            return;

    //        activePopupInstance.Show(shopItem.Item.ItemImage, shopItem.Item.ItemName, shopItem.Quantity, shopItem.Item.Price, index, shopItemSO, playerInventory);
    //        return;
    //    }

    //    int newIndex = uiItems.IndexOf(uIShopItem);
    //    if (newIndex == -1)
    //        return;

    //    activePopupInstance = Instantiate(purchasePopupPrefab, contentRectTransform);

    //    Items newItem = shopItemSO.GetItemAt(newIndex);
    //    if (newItem == null || newItem.Item == null)
    //        return;

    //    activePopupInstance.Show(newItem.Item.ItemImage, newItem.Item.ItemName, newItem.Quantity, newItem.Item.Price, newIndex, shopItemSO, playerInventory);

    //    activePopupInstance.OnClose += () => activePopupInstance = null;
    //}

    

    protected override void OnLeftClick(ShopItemUI itemUI)
    {
        int index = uiItems.IndexOf(itemUI);
        if (index == -1)
            return;

        Items shopItem = shopItemSO.GetItemAt(index);
        if (shopItem == null || shopItem.Item == null)
            return;

        InvokeDescriptionRequested(index);

        if (activePopupInstance)
            activePopupInstance.Show(shopItem.Item.ItemImage, shopItem.Item.ItemName, shopItem.Quantity, shopItem.Item.Price, index, shopItemSO, playerInventory);
    }

    protected override void OnRightClick(ShopItemUI itemUI)
    {
        if (activePopupInstance)
        {
            int index = uiItems.IndexOf(itemUI);
            if (index == -1)
                return;

            Items shopItem = shopItemSO.GetItemAt(index);
            if (shopItem == null || shopItem.Item == null)
                return;

            activePopupInstance.Show(shopItem.Item.ItemImage, shopItem.Item.ItemName, shopItem.Quantity, shopItem.Item.Price, index, shopItemSO, playerInventory);
            return;
        }

        int newIndex = uiItems.IndexOf(itemUI);
        if (newIndex == -1)
            return;

        activePopupInstance = Instantiate(purchasePopupPrefab, contentRectTransform);

        Items newItem = shopItemSO.GetItemAt(newIndex);
        if (newItem == null || newItem.Item == null)
            return;

        activePopupInstance.Show(newItem.Item.ItemImage, newItem.Item.ItemName, newItem.Quantity, newItem.Item.Price, newIndex, shopItemSO, playerInventory);

        GameManager.Instance.OnSwitchUI += ClosePopup;
    }

    public override void Clear()
    {
        base.Clear();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnSwitchUI += ClosePopup;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnSwitchUI -= ClosePopup;
    }
}
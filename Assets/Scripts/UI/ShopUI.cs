using System;
using UnityEngine;

public class ShopUI : BaseInventoryUI<ShopItemUI>
{
    [SerializeField] private ShopItemUI itemPrefab;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private ShopSO shopItemSO;

    public event Action<int> OnItemLeftClicked;
    public event Action<int> OnItemRightClicked;

    protected override void Awake()
    {
        base.Awake();
        itemDescription.ResetDescription();
    }

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

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (itemIndex < uiItems.Count)
            uiItems[itemIndex].SetItem(itemImage, itemQuantity);
    }

    protected override void OnLeftClick(ShopItemUI itemUI)
    {
        int index = uiItems.IndexOf(itemUI);
        if (index == -1)
            return;

        Items shopItem = shopItemSO.GetItemAt(index);
        if (shopItem == null || shopItem.Item == null)
            return;

        InvokeDescriptionRequested(index);

        if (ActivePopupUIInstance.gameObject.activeInHierarchy)
            ActivePopupUIInstance.Show(shopItem.Item.ItemImage, shopItem.Item.ItemName, shopItem.Quantity, shopItem.Item.Price, index);
    }

    protected override void OnRightClick(ShopItemUI itemUI)
    {
        PurchasePopupUI popup = ActivePopupUIInstance;

        if (popup.gameObject.activeInHierarchy)
        {
            int index = uiItems.IndexOf(itemUI);
            if (index == -1) return;

            Items shopItem = shopItemSO.GetItemAt(index);
            if (shopItem == null || shopItem.Item == null)
                return;

            popup.Show(shopItem.Item.ItemImage, shopItem.Item.ItemName, shopItem.Quantity, shopItem.Item.Price, index);
            return;
        }

        int newIndex = uiItems.IndexOf(itemUI);
        if (newIndex == -1) 
            return;

        popup.gameObject.SetActive(true);
        Debug.Log(popup.gameObject.activeInHierarchy);

        Items newItem = shopItemSO.GetItemAt(newIndex);
        if (newItem == null || newItem.Item == null)
            return;

        popup.Show(newItem.Item.ItemImage, newItem.Item.ItemName, newItem.Quantity, newItem.Item.Price, newIndex);

        popup.OnPurchaseConfirmed += GameManager.Instance.GetShopController().OnPurchaseConfirmed;
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
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private ShopItemUI itemPrefab;
    [SerializeField] private RectTransform contentRectTransform;
    [SerializeField] private UiItemDescription itemDescription;

    [SerializeField] private PurchasePopup purchasePopupPrefab;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private ShopSO shopItemSO;

    [SerializeField] private TMP_Text moneyText;

    List<ShopItemUI> uiShopItems = new List<ShopItemUI>();

    private PurchasePopup activePopupInstance;

    public event Action<int> OnDescriptionRequested, OnItemActionRequested;

    private void Awake()
    {
        itemDescription.ResetDescription();
        purchasePopupPrefab.gameObject.SetActive(false);

    }

    // Initialize the list of items in the shop with a list
    public void InitializeShopUIList(int shopListSize)
    {
        for (int i = 0; i < shopListSize; i++)
        {
            ShopItemUI uiShopItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiShopItem.transform.SetParent(contentRectTransform);
            uiShopItems.Add(uiShopItem);

            uiShopItem.OnItemClicked += UiShopItem_OnItemClicked;
            uiShopItem.OnRightMouseButtonClicked += UiShopItem_OnRightMouseButtonClicked;
        }
    }

    private void ShopUI_OnLeftClick()
    {
        throw new NotImplementedException();
    }

    // Update the data with image and quantity
    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (itemIndex < uiShopItems.Count)
            uiShopItems[itemIndex].SetItem(itemImage, itemQuantity);
    }

    // Event called when clicked on item in the UI
    public void UiShopItem_OnItemClicked(ShopItemUI uIShopItem)
    {
        int index = uiShopItems.IndexOf(uIShopItem);
        if (index == -1)
            return;

        Items shopItem = shopItemSO.GetItemAt(index);
        if (shopItem == null || shopItem.Item == null)
            return;

        OnDescriptionRequested?.Invoke(index);

        if (activePopupInstance)
            activePopupInstance.Show(shopItem.Item.ItemImage, shopItem.Item.ItemName, shopItem.Quantity, shopItem.Item.Price, index, shopItemSO, playerInventory);
    }

    public void UiShopItem_OnRightMouseButtonClicked(ShopItemUI uIShopItem)
    {
        if (activePopupInstance)
        {
            int index = uiShopItems.IndexOf(uIShopItem);
            if (index == -1)
                return;

            Items shopItem = shopItemSO.GetItemAt(index);
            if (shopItem == null || shopItem.Item == null)
                return;

            activePopupInstance.Show(shopItem.Item.ItemImage, shopItem.Item.ItemName, shopItem.Quantity, shopItem.Item.Price, index, shopItemSO, playerInventory);
            return;
        }

        int newIndex = uiShopItems.IndexOf(uIShopItem);
        if (newIndex == -1)
            return;

        activePopupInstance = Instantiate(purchasePopupPrefab, contentRectTransform);

        Items newItem = shopItemSO.GetItemAt(newIndex);
        if (newItem == null || newItem.Item == null)
            return;

        activePopupInstance.Show(newItem.Item.ItemImage, newItem.Item.ItemName, newItem.Quantity, newItem.Item.Price, newIndex, shopItemSO, playerInventory);

        activePopupInstance.OnClose += () => activePopupInstance = null;
    }

    public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description, int price)
    {
        if (itemIndex < 0 || itemIndex >= uiShopItems.Count)
            return;

        itemDescription.SetDescription(itemImage, name, description, price);
    }

    public void ClosePopup()
    {
        if (activePopupInstance != null)
        {
            Destroy(activePopupInstance.gameObject);
            activePopupInstance = null;
        }
    }
}
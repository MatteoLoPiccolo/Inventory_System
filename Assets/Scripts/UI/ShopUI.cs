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

    private bool isPopupActive = false;

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

    // Update the data with image and quantity
    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (itemIndex < uiShopItems.Count) {
            uiShopItems[itemIndex].SetItem(itemImage, itemQuantity);
        }
    }

    // Event called when clicked on item in the UI
    private void UiShopItem_OnItemClicked(ShopItemUI uIShopItem)
    {
        int index = uiShopItems.IndexOf(uIShopItem);
        if (index == -1)
            return;

        OnDescriptionRequested?.Invoke(index);
    }

    private void UiShopItem_OnRightMouseButtonClicked(ShopItemUI uIShopItem)
    {
        if (isPopupActive)
            return;

        int index = uiShopItems.IndexOf(uIShopItem);
        if (index == -1)
            return;

        PurchasePopup popupInstance = Instantiate(purchasePopupPrefab, contentRectTransform);
        isPopupActive = true;

        // Read the item data
        Items shopItem = shopItemSO.GetItemAt(index);
        if (shopItem == null || shopItem.Item == null)
            return;

        popupInstance.Show(shopItem.Item.ItemImage, shopItem.Item.ItemName, shopItem.Quantity, shopItem.Item.Price, index, shopItemSO, playerInventory);

        popupInstance.OnClose += () => isPopupActive = false;
    }

    public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description, int price)
    {
        if (itemIndex < 0 || itemIndex >= uiShopItems.Count)
            return;

        itemDescription.SetDescription(itemImage, name, description, price);
    }

    public void UpdateMoneyText(int money)
    {
        moneyText.text = "Money: " + money.ToString();
    }
}
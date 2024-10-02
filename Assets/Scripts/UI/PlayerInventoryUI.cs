using System;
using System.Linq;
using UnityEngine;

public class PlayerInventoryUI : BaseInventoryUI<InventoryItemUI>
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PurchasePopupUI purchasePopupPrefab;
    [SerializeField] private InventoryItemUI itemPrefab;

    [SerializeField] private ShopSO shopItemSO;

    private void Start()
    {
        playerInventory.OnInventoryItemAdded += AddItemUI;
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        foreach (var item in playerInventory.GetInventoryItems())
        {
            AddItemUI(item.Key, item.Value);
        }
    }

    public void AddItemUI(ItemSO item, int quantity)
    {
        InventoryItemUI existingItemUI = null;

        foreach (var uiItem in uiInventoryItems)
        {
            if (uiItem.Item == item)
            {
                existingItemUI = uiItem;
                break;
            }
        }

        if (existingItemUI != null)
        {
            existingItemUI.UpdateQuantity();
        }
        else
        {
            InventoryItemUI newItem = Instantiate(itemPrefab, contentRectTransform);
            newItem.SetItem(item, quantity);
            newItem.gameObject.SetActive(true);
            newItem.OnItemClicked += OnLeftClick;
            newItem.OnRightMouseButtonClicked += OnRightClick;
            uiInventoryItems.Add(newItem);
        }
    }

    private void UnsubscribeFromOnInventoryItemAdded()
    {
        Debug.Log("UnsubscribeFromOnInventoryItemAdded is called");
        playerInventory.OnInventoryItemAdded -= AddItemUI; ;
    }

    public override void Clear()
    {
        foreach (var item in uiInventoryItems)
        {
            Destroy(item.gameObject);
        }

        uiInventoryItems.Clear();
        base.Clear();
    }

    public void UpdateInventoryItemDescription(int itemIndex, Sprite itemImage, string name, string description, int price)
    {
        if (itemIndex < 0 || itemIndex >= uiInventoryItems.Count)
            return;

        itemDescription.SetDescription(itemImage, name, description, price);
    }

    public void UpdateData(int itemIndex, ItemSO item, int itemQuantity)
    {
        if (itemIndex < uiInventoryItems.Count)
            uiInventoryItems[itemIndex].SetItem(item, itemQuantity);
    }

    protected override void OnLeftClick(InventoryItemUI itemUI)
    {
        Debug.Log("Left click detected in Inventory");

        int index = uiInventoryItems.IndexOf(itemUI);
        if (index == -1)
            return;

        var playerItems = playerInventory.GetInventoryItems();
        var playerItem = playerItems.ElementAtOrDefault(index);

        if (playerItem.Key == null || playerItem.Value <= 0)
            return;

        InvokeDescriptionRequested(index);

        if (ActivePopupUIInstance.gameObject.activeInHierarchy)
            ActivePopupUIInstance.Show(playerItem.Key.ItemImage, playerItem.Key.ItemName, playerItem.Value, playerItem.Key.Price, index);
    }

    protected override void OnRightClick(InventoryItemUI itemUI)
    {
        Debug.Log("Right click detected in Inventory");

        int index = uiInventoryItems.IndexOf(itemUI);
        if (index == -1)
            return;
    }
}
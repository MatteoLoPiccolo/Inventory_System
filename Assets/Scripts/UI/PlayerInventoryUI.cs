using System.Linq;
using UnityEngine;

public class PlayerInventoryUI : BaseInventoryUI<InventoryItemUI>
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItemUI itemPrefab;

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

    public override void Clear()
    {
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
        int index = uiInventoryItems.IndexOf(itemUI);
        if (index == -1)
            return;

        var playerItems = playerInventory.GetInventoryItems();
        var playerItem = playerItems.ElementAtOrDefault(index);

        if (playerItem.Key == null || playerItem.Value <= 0)
            return;

        InvokeDescriptionRequested(index);

        if (activeSellPopupUIInstance.gameObject.activeInHierarchy)
            ActiveSellPopupUIInstance.Show(playerItem.Key.ItemImage, playerItem.Key.ItemName, playerItem.Value, playerItem.Key.Price, index);
    }

    protected override void OnRightClick(InventoryItemUI itemUI)
    {
        int index = uiInventoryItems.IndexOf(itemUI);
        if (index == -1)
            return;

        SellPopupUI popup = ActiveSellPopupUIInstance;

        if (popup.gameObject.activeInHierarchy)
            return;

        int newIndex = uiInventoryItems.IndexOf(itemUI);
        if (newIndex == -1)
            return;

        popup.gameObject.SetActive(true);

        var playerItems = playerInventory.GetInventoryItems();
        var playerItem = playerItems.ElementAtOrDefault(index);

        if (playerItem.Key == null || playerItem.Value <= 0)
            return;

        popup.Show(playerItem.Key.ItemImage, playerItem.Key.ItemName, playerItem.Value, playerItem.Key.Price, index);

        popup.OnSellConfirmed += GameManager.Instance.InventoryController.OnSellConfirmed;
    }
}
using UnityEngine;

public class PlayerInventoryUI : BaseInventoryUI<InventoryItemUI>
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PurchasePopupUI purchasePopupPrefab;
    [SerializeField] private InventoryItemUI itemPrefab;

    private void Start()
    {
        playerInventory.OnInventoryChanged += UpdateInventoryUI;
        InitializeUIList(playerInventory.GetPlayerItems().Count);
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        Clear();

        foreach (var item in playerInventory.GetPlayerItems())
            AddItemUI(item.Item, item.Quantity);
    }

    public void AddItemUI(ItemSO item, int quantity)
    {
        InventoryItemUI newItem = Instantiate(itemPrefab, contentRectTransform);
        newItem.SetItem(item.ItemImage, quantity);
        newItem.gameObject.SetActive(true);
        newItem.OnItemClicked += OnLeftClick;
        newItem.OnRightMouseButtonClicked += OnRightClick;
        uiItems.Add(newItem);
    }

    public override void Clear()
    {
        base.Clear();
    }

    protected override void InitializeUIList(int listSize)
    {
        for (int i = 0; i < listSize; i++)
        {
            InventoryItemUI inventoryItemUI = Instantiate(itemPrefab, contentRectTransform);
            inventoryItemUI.OnItemClicked += OnLeftClick;
            inventoryItemUI.OnRightMouseButtonClicked += OnRightClick;
            uiItems.Add(inventoryItemUI);
        }
    }

    public void InitializeList(int listSize)
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

    protected override void OnLeftClick(InventoryItemUI itemUI)
    {
        Debug.Log("Left click detected in Inventory");

        int index = uiItems.IndexOf(itemUI);
        if (index == -1) 
            return;

        InvokeDescriptionRequested(index);
    }

    protected override void OnRightClick(InventoryItemUI itemUI)
    {
        Debug.Log("Right click detected in Inventory");

        int index = uiItems.IndexOf(itemUI);
        if (index == -1) 
            return;

        InvokeDescriptionRequested(index);
    }
}
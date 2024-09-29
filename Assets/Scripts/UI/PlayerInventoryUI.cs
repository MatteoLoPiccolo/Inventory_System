using TMPro;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour //BaseInventoryUI<ShopItemUI>
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItemUI itemPrefab;
    [SerializeField] private RectTransform contentRectTransform;
    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private UIController uIController;

    private void Start()
    {
        playerInventory.OnInventoryChanged += UpdateInventoryUI;
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
        newItem.SetItem(item, quantity);
    }

    public void Clear()
    {
        foreach (Transform child in contentRectTransform)
            Destroy(child.gameObject);
    }
}
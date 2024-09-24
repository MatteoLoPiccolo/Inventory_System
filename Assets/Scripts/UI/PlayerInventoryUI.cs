using UnityEngine;
using TMPro;

public class PlayerInventoryUI : MonoBehaviour
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
        Debug.Log("UpdateInventoryUI is called!");

        Clear();

        foreach (var item in playerInventory.GetPlayerItems())
        {
            Debug.Log($"Item.Item is : {item.Item}");
            Debug.Log($"Item.Quantity is : {item.Quantity}");
            AddItemUI(item.Item, item.Quantity);
        }
    }

    public void AddItemUI(ItemSO item, int quantity)
    {
        InventoryItemUI newItem = Instantiate(itemPrefab, contentRectTransform);
        Debug.Log($"Instantiated itemPrefab with name: {newItem.name}");
        Debug.Log($"itemPrefab.sprite is: {item.ItemImage}");
        newItem.SetItem(item, quantity);
    }

    public void Clear()
    {
        foreach (Transform child in contentRectTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
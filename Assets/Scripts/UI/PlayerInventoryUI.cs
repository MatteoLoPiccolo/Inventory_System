using UnityEngine;
using TMPro;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItemUI itemPrefab;
    [SerializeField] private RectTransform contentRectTransform;
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        playerInventory.OnInventoryChanged += UpdateInventoryUI;
    }

    private void UpdateInventoryUI()
    {
        Debug.Log("UpdateInventoryUI is called!");
    }

    public void AddItem(ItemSO item, int quantity)
    {
        InventoryItemUI newItem = Instantiate(itemPrefab);
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
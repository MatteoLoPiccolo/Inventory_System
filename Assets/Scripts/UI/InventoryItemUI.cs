using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantityText;

    public Image ItemImage
    {
        get { return itemImage; }
    }

    public void SetItem(ItemSO item, int quantity)
    {
        //itemImage.sprite = item.ItemImage;
        //quantityText.text = quantity.ToString();

        if (item == null)
        {
            Debug.LogError("ItemSO is null in SetItem");
            return;
        }

        if (itemImage != null)
            itemImage.sprite = item.ItemImage;
        else
            Debug.LogError("Item image is not assigned in InventoryItemUI");

        if (quantityText != null)
            quantityText.text = quantity.ToString();
        else
            Debug.LogError("Item quantity text is not assigned in InventoryItemUI");

        Debug.Log($"SetItem called with item: {item.ItemName}, quantity: {quantity}");
    }
}
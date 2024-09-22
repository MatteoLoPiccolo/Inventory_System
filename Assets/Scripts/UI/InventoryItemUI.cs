using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantityText;

    public void SetItem(ItemSO item, int quantity)
    {
        itemImage.sprite = item.ItemImage;
        quantityText.text = quantity.ToString();
    }
}
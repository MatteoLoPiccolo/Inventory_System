using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemQuantityText;

    public ItemSO Item { get; private set; }

    public int Quantity
    {
        get => int.Parse(itemQuantityText.text);
        set => itemQuantityText.text = value.ToString();
    }

    public event Action<InventoryItemUI> OnItemClicked;
    public event Action<InventoryItemUI> OnRightMouseButtonClicked;

    public void SetItem(ItemSO item, int quantity)
    {
        if (item != null)
        {
            Item = item;
            itemImage.sprite = item.ItemImage;
            itemImage.gameObject.SetActive(true);
        }

        Quantity = quantity;
    }

    public void UpdateQuantity()
    {
        Quantity ++;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            OnRightMouseButtonClicked?.Invoke(this);
        else
            OnItemClicked?.Invoke(this);
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemQuantityText;

    public event Action<InventoryItemUI> OnItemClicked;
    public event Action<InventoryItemUI> OnRightMouseButtonClicked;

    public Image ItemImage
    {
        get { return itemImage; }
    }

    public void SetItem(Sprite spriteImage, int quantity)
    {
        if (itemImage != null)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = spriteImage;
        }
        if (itemQuantityText != null)
            itemQuantityText.text = quantity.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            OnRightMouseButtonClicked?.Invoke(this);
        else
            OnItemClicked?.Invoke(this);
    }
}
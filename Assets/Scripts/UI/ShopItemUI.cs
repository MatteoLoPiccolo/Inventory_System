using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemQuantityText;

    public event Action<ShopItemUI> OnItemClicked, OnRightMouseButtonClicked;

    private void Awake()
    {
        ResetItem();
    }

    private void ResetItem()
    {
        if (itemImage != null)
        {
            itemImage.gameObject.SetActive(false);
        }
        if (itemQuantityText != null)
        {
            itemQuantityText.text = string.Empty;
        }
    }

    public void SetItem(Sprite spriteImage, int quantity)
    {
        if (itemImage != null)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = spriteImage;
        }
        if (itemQuantityText != null)
        {
            itemQuantityText.text = quantity.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseButtonClicked?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellPopupUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text sellText;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button backButton;
    [SerializeField] private UIController uIController;
    [SerializeField] private ShopUI shopUI;

    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerInventoryUI playerInventoryUI;

    public event Action<int> OnSellConfirmed;

    private int itemIndex;

    public void Show(Sprite itemSprite, string itemName, int quantity, int price, int index)
    {
        itemImage.sprite = itemSprite;
        itemNameText.text = $"{itemName}";
        sellText.text = $"Price: {price}";
        itemIndex = index;

        gameObject.SetActive(true);
    }

    private void ConfirmedSell()
    {
        Debug.Log("Buy button clicked, invoking purchase confirmed");
        Debug.Log($"Confirming purchase for item index: {itemIndex}");
        OnSellConfirmed?.Invoke(itemIndex);
    }

    private void OnEnable()
    {
        sellButton.onClick.AddListener(ConfirmedSell);
        backButton.onClick.AddListener(ClosePopup);
    }

    private void OnDisable()
    {
        sellButton.onClick.RemoveListener(ConfirmedSell);
        backButton.onClick.RemoveListener(ClosePopup);
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
        OnSellConfirmed -= GameManager.Instance.InventoryController.OnSellConfirmed;
    }
}

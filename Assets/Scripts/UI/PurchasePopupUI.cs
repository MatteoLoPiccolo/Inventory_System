using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePopupUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button backButton;
    [SerializeField] private UIController uIController;
    [SerializeField] private ShopUI shopUI;

    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerInventoryUI playerInventoryUI;

    public event Action<int> OnPurchaseConfirmed;

    private int itemIndex;

    public void Show(Sprite itemSprite, string itemName, int quantity, int price, int index)
    {
        itemImage.sprite = itemSprite;
        itemNameText.text = $"{itemName}";
        priceText.text = $"Price: {price}";
        itemIndex = index;

        gameObject.SetActive(true);
    }

    private void OnConfirmPurchase()
    {
        Debug.Log("Buy button clicked, invoking purchase confirmed");
        Debug.Log($"Confirming purchase for item index: {itemIndex}");
        OnPurchaseConfirmed?.Invoke(itemIndex);
    }

    private void OnEnable()
    {
        buyButton.onClick.AddListener(OnConfirmPurchase);
        backButton.onClick.AddListener(ClosePopup);
    }

    private void OnDisable()
    {
        buyButton.onClick.RemoveListener(OnConfirmPurchase);
        backButton.onClick.RemoveListener(ClosePopup);
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
        OnPurchaseConfirmed -= GameManager.Instance.ShopController.OnPurchaseConfirmed;
    }
}
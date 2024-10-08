using InventorySystemController;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystemUI
{
    public class PurchasePopupUI : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button backButton;

        private int itemIndex;

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

        public event Action<int> OnPurchaseConfirmed;

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
            OnPurchaseConfirmed?.Invoke(itemIndex);
        }

        public void ClosePopup()
        {
            gameObject.SetActive(false);
            OnPurchaseConfirmed -= GameManager.Instance.ShopController.OnPurchaseConfirmed;
        }
    }
}
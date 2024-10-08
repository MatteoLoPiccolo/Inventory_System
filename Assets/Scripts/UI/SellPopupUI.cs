using InventorySystemController;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystemUI
{
    public class SellPopupUI : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private TMP_Text sellText;
        [SerializeField] private Button sellButton;
        [SerializeField] private Button backButton;

        private int itemIndex;

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

        public event Action<int> OnSellConfirmed;

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
            OnSellConfirmed?.Invoke(itemIndex);
        }

        public void ClosePopup()
        {
            gameObject.SetActive(false);
            OnSellConfirmed -= GameManager.Instance.InventoryController.OnSellConfirmed;
        }
    }
}
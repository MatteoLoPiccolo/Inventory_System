using Controller;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PurchasePopupUI : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button backButton;

        private int itemIndex;

        #endregion

        #region Obj life cycle

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

        #endregion

        #region Events

        public event Action<int> OnPurchaseConfirmed;

        #endregion

        #region Functions

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

        #endregion
    }
}
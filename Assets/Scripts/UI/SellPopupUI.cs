using Controller;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SellPopupUI : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private TMP_Text sellText;
        [SerializeField] private Button sellButton;
        [SerializeField] private Button backButton;

        private int itemIndex;

        #endregion

        #region Obj life cycle

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

        #endregion

        #region Events

        public event Action<int> OnSellConfirmed;

        #endregion

        #region Functions

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

        #endregion
    }
}
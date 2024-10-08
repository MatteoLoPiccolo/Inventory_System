using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystemUI
{
    public abstract class ItemPopupUI : MonoBehaviour
    {
        [SerializeField] protected Image itemImage;
        [SerializeField] protected TMP_Text itemNameText;
        [SerializeField] protected TMP_Text priceText;
        [SerializeField] protected Button actionButton;
        [SerializeField] protected Button backButton;

        protected int itemIndex;

        protected virtual void OnEnable()
        {
            actionButton.onClick.AddListener(OnConfirmAction);
            backButton.onClick.AddListener(ClosePopup);
        }

        protected virtual void OnDisable()
        {
            actionButton.onClick.RemoveListener(OnConfirmAction);
            backButton.onClick.RemoveListener(ClosePopup);
        }

        protected abstract void OnConfirmAction();

        public virtual void Show(Sprite itemSprite, string itemName, int price, int index)
        {
            itemImage.sprite = itemSprite;
            itemNameText.text = $"{itemName}";
            priceText.text = $"Price: {price}";
            itemIndex = index;

            gameObject.SetActive(true);
        }

        public virtual void ClosePopup()
        {
            gameObject.SetActive(false);
        }
    }
}
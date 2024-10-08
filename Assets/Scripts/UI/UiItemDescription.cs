using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystemUI
{
    public class UiItemDescription : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text priceText;

        private void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription()
        {
            itemImage.gameObject.SetActive(false);
            titleText.text = string.Empty;
            descriptionText.text = string.Empty;
            priceText.text = string.Empty;
        }

        public void SetDescription(Sprite spriteImage, string itemName, string itemDescription, int itemPrice)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = spriteImage;
            titleText.text = itemName;
            descriptionText.text = itemDescription;
            priceText.text = $"Price: {itemPrice}";
        }
    }
}
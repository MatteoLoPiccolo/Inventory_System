using Player;
using TMPro;
using UnityEngine;

namespace Controller
{
    public class UIController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private CanvasGroup shopCanvasGroup;
        [SerializeField] private CanvasGroup playerInventoryCanvasGroup;
        [SerializeField] private TMP_Text shopText;
        [SerializeField] private TMP_Text inventoryText;
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private TMP_Text playerMoneyText;
        [SerializeField] private TMP_Text shopMoneyText;

        private bool isShopOpen = false;

        #endregion

        #region Obj life cycle

        private void Start()
        {
            GameManager.Instance.PlayerInventory.OnMoneyChanged += UpdatePlayerMoneyUI;
            GameManager.Instance.ShopController.OnMoneyChanged += UpdateShopMoneyUI;

            ShowPlayerInventory();
            SetTextMoneyUI();
        }

        #endregion

        #region Functions

        public void ShowShop()
        {
            SetCanvasGroupVisibility(shopCanvasGroup, true);
            SetCanvasGroupVisibility(playerInventoryCanvasGroup, false);

            shopText.gameObject.SetActive(true);
            inventoryText.gameObject.SetActive(false);

            isShopOpen = true;

            GameManager.Instance.InventoryController.GetInventoryUI().CloseSellPopup();
        }

        public void ShowPlayerInventory()
        {
            SetCanvasGroupVisibility(shopCanvasGroup, false);
            SetCanvasGroupVisibility(playerInventoryCanvasGroup, true);

            shopText.gameObject.SetActive(false);
            inventoryText.gameObject.SetActive(true);

            isShopOpen = false;

            GameManager.Instance.ShopController.GetShopUI().ClosePurchasePopup();
        }

        private void SetCanvasGroupVisibility(CanvasGroup canvasGroup, bool isVisible)
        {
            canvasGroup.alpha = isVisible ? 1 : 0;
            canvasGroup.interactable = isVisible;
            canvasGroup.blocksRaycasts = isVisible;
        }

        public void ToggleShop()
        {
            if (isShopOpen)
                ShowPlayerInventory();
            else
                ShowShop();
        }

        public void SetTextMoneyUI()
        {
            if (playerMoneyText)
                playerMoneyText.text = $"Player $: {GameManager.Instance.PlayerInventory.InventoryMoney}";
            else
                Debug.LogError("playerMoneyText is null");

            if (shopMoneyText)
                shopMoneyText.text = $"Shop $ : {GameManager.Instance.ShopController.ShopMoney}";
            else
                Debug.LogError("shopMoneyText is null");
        }

        public void UpdatePlayerMoneyUI(int newMoney)
        {
            playerMoneyText.text = $"Player $: {newMoney}";
        }

        public void UpdateShopMoneyUI(int newMoney)
        {
            shopMoneyText.text = $"Shop $ : {newMoney}";
        }

        #endregion
    }
}
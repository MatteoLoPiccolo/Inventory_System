using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup shopCanvasGroup;
    [SerializeField] private CanvasGroup playerInventoryCanvasGroup;
    [SerializeField] private TMP_Text shopText;
    [SerializeField] private TMP_Text inventoryText;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerInventoryUI playerInventoryUI;
    [SerializeField] private TMP_Text playerMoneyText;
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private TMP_Text shopMoneyText;

    private bool isShopOpen = false;

    private void Start()
    {
        playerInventory.OnMoneyChanged += UpdatePlayerMoneyUI;
        GameManager.Instance.ShopController.OnMoneyChanged += UpdateShopMoneyUI;

        ShowPlayerInventory();
        SetTextMoneyUI();
    }

    public void ShowShop()
    {
        SetCanvasGroupVisibility(shopCanvasGroup, true);
        SetCanvasGroupVisibility(playerInventoryCanvasGroup, false);

        shopText.gameObject.SetActive(true);
        inventoryText.gameObject.SetActive(false);

        isShopOpen = true;
    }

    public void ShowPlayerInventory()
    {
        SetCanvasGroupVisibility(shopCanvasGroup, false);
        SetCanvasGroupVisibility(playerInventoryCanvasGroup, true);

        shopText.gameObject.SetActive(false);
        inventoryText.gameObject.SetActive(true);

        isShopOpen = false;

        GameManager.Instance.ShopController.GetShopUI().ClosePopup();
    }

    private void SetCanvasGroupVisibility(CanvasGroup canvasGroup, bool isVisible)
    {
        canvasGroup.alpha = isVisible ? 1 : 0; // Check the transparency
        canvasGroup.interactable = isVisible;  // Enable or disable the interaction
        canvasGroup.blocksRaycasts = isVisible; // Enable or disable raycast (for clic)
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
}
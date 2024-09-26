using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup shopCanvasGroup;
    [SerializeField] private CanvasGroup playerInventoryCanvasGroup;
    [SerializeField] private TMP_Text shopText;
    [SerializeField] private TMP_Text inventoryText;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerInventoryUI playerInventoryUI;
    [SerializeField] private TMP_Text playerMoneyText;
    [SerializeField] private ShopController shopController;
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private TMP_Text shopMoneyText;

    private bool isShopOpen = false;

    private void Start()
    {
        playerInventory.OnMoneyChanged += UpdatePlayerMoneyUI;
        shopController.OnMoneyChanged += UpdateShopMoneyUI;
        
        ShowPlayerInventory();
        SetTextMoneyUI();
    }

    // Show the shop and hide the inventory
    public void ShowShop()
    {
        SetCanvasGroupVisibility(shopCanvasGroup, true);
        SetCanvasGroupVisibility(playerInventoryCanvasGroup, false);

        shopText.gameObject.SetActive(true);
        inventoryText.gameObject.SetActive(false);

        isShopOpen = true;
    }

    // Show inventory and hide the shop
    public void ShowPlayerInventory()
    {
        SetCanvasGroupVisibility(shopCanvasGroup, false);
        SetCanvasGroupVisibility(playerInventoryCanvasGroup, true);

        shopText.gameObject.SetActive(false);
        inventoryText.gameObject.SetActive(true);

        isShopOpen = false;
    }

    // Use CanvasGroup to manage visibility and interaction
    private void SetCanvasGroupVisibility(CanvasGroup canvasGroup, bool isVisible)
    {
        canvasGroup.alpha = isVisible ? 1 : 0; // Check the transparency
        canvasGroup.interactable = isVisible;  // Enable or disable the interaction
        canvasGroup.blocksRaycasts = isVisible; // Enable or disable raycast (for clic)
    }

    // If the shop is visible, the inventory is invisible and viceversa
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
            playerMoneyText.text = $"Player $: {playerInventory.InventoryMoney}";
        else
            Debug.LogError("playerMoneyText is null");

        if (shopMoneyText)
            shopMoneyText.text = $"Shop $ : {shopController.ShopMoney}";
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
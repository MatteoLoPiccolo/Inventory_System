using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePopup : MonoBehaviour
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

    private int itemIndex;
    private ShopSO shopData;

    private ItemSO item;

    public event Action OnClose;

    public void Show(Sprite itemSprite, string itemName, int quantity, int price, int index, ShopSO shopData, PlayerInventory playerInventory)
    {
        itemImage.sprite = itemSprite;
        itemNameText.text = $"{itemName} x{quantity}";
        priceText.text = $"Price: {price}";
        itemIndex = index;

        this.shopData = shopData;
        this.playerInventory = playerInventory;

        gameObject.SetActive(true);
    }

    private void OnConfirmPurchase()
    {
        Items item = shopData.GetItemAt(itemIndex);

        if (playerInventory.CanAfford(item.Item.Price))
        {
            int inventoryNewMoneyAmount = playerInventory.GetMoney() - item.Item.Price;
            int newShopMoneyAMount = GameManager.Instance.GetShopController().GetMoney() + item.Item.Price;

            playerInventory.SetMoney(inventoryNewMoneyAmount);
            GameManager.Instance.GetShopController().SetMoney(newShopMoneyAMount);

            playerInventory.AddItem(item.Item, item.Quantity);

            OnClose?.Invoke();
        }
        else
            Debug.Log("You don't have enough money!");
    }

    private void OnClosePopup()
    {
        gameObject.SetActive(false);
        OnClose?.Invoke();
    }

    private void OnEnable()
    {
        buyButton.onClick.AddListener(OnConfirmPurchase);
        backButton.onClick.AddListener(OnClosePopup);
    }

    private void OnDisable()
    {
        buyButton.onClick.RemoveListener(OnConfirmPurchase);
        backButton.onClick.RemoveListener(OnClosePopup);
    }
}
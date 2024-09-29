using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePopupUI : MonoBehaviour
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

    //public event Action OnClose;

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
            int playerNewMoneyAmount = playerInventory.GetMoney() - item.Item.Price;
            int shopNewMoneyAmount = GameManager.Instance.GetShopController().ShopMoney + item.Item.Price;

            playerInventory.SetMoney(playerNewMoneyAmount);
            GameManager.Instance.GetShopController().SetMoney(shopNewMoneyAmount);

            playerInventory.AddItem(item.Item, item.Quantity);

            //GameManager.Instance.GetShopController().GetShopUI().ClosePopup();

            //OnClose?.Invoke();
        }
        else
            Debug.Log("You don't have enough money!");
    }

    //private void OnClosePopup()
    //{
    //    gameObject.SetActive(false);
    //    OnClose?.Invoke();
    //}

    private void OnEnable()
    {
        buyButton.onClick.AddListener(OnConfirmPurchase);
        backButton.onClick.AddListener(GameManager.Instance.GetShopController().GetShopUI().ClosePopup);
    }

    private void OnDisable()
    {
        buyButton.onClick.RemoveListener(OnConfirmPurchase);
        backButton.onClick.RemoveListener(GameManager.Instance.GetShopController().GetShopUI().ClosePopup);
    }
}
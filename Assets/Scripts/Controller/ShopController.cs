using System;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private ShopSO shopData;
    [SerializeField] private UIController uiManager;
    [SerializeField] private int shopItemsListSize = 7;
    [SerializeField] private int shopMoney;

    public event Action<int> OnMoneyChanged;

    public int ShopMoney
    {
        get => shopMoney;

        private set
        {
            shopMoney = value;
            OnMoneyChanged?.Invoke(shopMoney);
        }
    }

    public int GetMoney()
    { 
        return ShopMoney; 
    }

    public void SetMoney(int newAmount)
    {
        ShopMoney = newAmount;
    }

    private void Awake()
    {
        shopMoney = 1000;
        shopData.Initialize(shopItemsListSize);
    }

    private void Start()
    {
        SetUpUI();
    }

    private void SetUpUI()
    {
        shopUI.InitializeShopUIList(shopItemsListSize);

        shopUI.OnDescriptionRequested += OnDescriptionRequested;

        foreach (var item in shopData.GetCurrentShopItemState())
            shopUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
    }

    private void OnDescriptionRequested(int itemIndex)
    {
        Items shopItem = shopData.GetItemAt(itemIndex);

        if(shopItem.IsEmpty)
            return;

        ItemSO item = shopItem.Item;
        shopUI.UpdateDescription(itemIndex, item.ItemImage, item.ItemName, item.Description, item.Price);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
            uiManager.ToggleShop();
    }
}
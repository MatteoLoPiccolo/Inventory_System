using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Items> playerInventoryItems;
    [SerializeField] private PlayerInventoryUI inventoryUI;
    [SerializeField] private int inventoryMoney;

    private Dictionary<ItemSO, int> inventory = new Dictionary<ItemSO, int>();

    public event Action<int> OnMoneyChanged;
    public event Action OnInventoryChanged;

    public int InventoryMoney
    {
        get => inventoryMoney;
        private set
        {
            inventoryMoney = value;
            OnMoneyChanged?.Invoke(inventoryMoney);
        }
    }

    private void Awake()
    {
        inventoryMoney = 100;
        ResetInventory();
    }

    private void Start()
    {
        playerInventoryItems = new List<Items>();
    }

    private void ResetInventory()
    {
        inventory.Clear();
        playerInventoryItems.Clear();
        inventoryUI.Clear();
    }

    public int GetMoney()
    {
        return InventoryMoney;
    }

    public void SetMoney(int newAmount)
    {
        InventoryMoney = newAmount;
    }

    public void AddItem(ItemSO item, int quantity)
    {
        if (inventory.ContainsKey(item))
            inventory[item] += quantity;
        else
            inventory[item] = quantity;

        Items existingItem = playerInventoryItems.Find(i => i.Item == item);

        if (existingItem != null)
            existingItem.ChangeQuantity(existingItem.Quantity + quantity);
        else
            playerInventoryItems.Add(new Items(item, quantity));

        OnInventoryChanged?.Invoke();
    }

    public void UpdateUI()
    {
        inventoryUI.Clear();

        foreach (var item in inventory)
            inventoryUI.AddItemUI(item.Key, item.Value);
    }

    public bool CanAfford(int cost)
    {
        return GetMoney() >= cost;
    }

    public List<Items> GetPlayerItems()
    {
        return playerInventoryItems;
    }
}
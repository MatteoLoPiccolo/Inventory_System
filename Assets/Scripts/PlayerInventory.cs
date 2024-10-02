using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private PlayerInventoryUI playerInventoryUI;
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private int inventoryMoney;

    private Dictionary<ItemSO, int> inventory = new Dictionary<ItemSO, int>();

    public event Action<int> OnMoneyChanged;
    public event Action<ItemSO, int> OnInventoryItemAdded;

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

    private void ResetInventory()
    {
        inventory.Clear();
        playerInventoryUI.Clear();
        UpdateUI();
    }

    public void AddItem(ItemSO item, int quantity)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] += quantity;
            Debug.Log($"Increased {item.ItemName} quantity by {quantity}. New quantity: {inventory[item]}.");
        }
        else
        {
            inventory[item] = quantity;
            Debug.Log($"Adding {quantity} of {item.ItemName} to inventory.");
        }

        OnInventoryItemAdded?.Invoke(item, inventory[item]);
    }


    public void RemoveItem(ItemSO item, int quantity)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] -= quantity;
            if (inventory[item] <= 0)
                inventory.Remove(item);

            OnInventoryItemAdded?.Invoke(item, inventory[item]);
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        playerInventoryUI.Clear();

        foreach (var item in inventory)
            playerInventoryUI.AddItemUI(item.Key, item.Value);
    }

    public bool CanAfford(int cost)
    {
        return GetMoney() >= cost;
    }

    public Dictionary<ItemSO, int> GetInventoryItems()
    {
        return inventory;
    }

    public int GetMoney()
    {
        return InventoryMoney;
    }

    public void SetMoney(int newAmount)
    {
        InventoryMoney = newAmount;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnSwitchUI += shopUI.ClosePopup;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnSwitchUI -= shopUI.ClosePopup;
    }
}
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

    private void ResetInventory()
    {
        inventory.Clear();
        playerInventoryUI.Clear();
        UpdateUI();
    }

    public void AddItem(ItemSO item, int quantity)
    {
        if (inventory.ContainsKey(item))
            inventory[item] += quantity;
        else
            inventory[item] = quantity;

        Debug.Log($"Adding {quantity} of {item.ItemName} to inventory.");

        OnInventoryChanged?.Invoke();
        UpdateUI();
    }

    public void RemoveItem(ItemSO item, int quantity)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] -= quantity;
            if (inventory[item] <= 0)
                inventory.Remove(item);

            OnInventoryChanged?.Invoke();
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

    //public List<Items> GetPlayerItems()
    //{
    //    List<Items> itemsList = new List<Items>();

    //    foreach (var kvp in inventory)
    //    {
    //        itemsList.Add(new Items(kvp.Key, kvp.Value));
    //    }

    //    return itemsList;
    //}

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
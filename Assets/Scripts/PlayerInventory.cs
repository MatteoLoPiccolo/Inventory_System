using InventorySystemModel;
using System;
using System.Collections.Generic;
using InventorySystemUI;
using UnityEngine;
using InventorySystemInterfaces;

namespace InventorySystemPlayer
{
    public class PlayerInventory : MonoBehaviour, IMoneyManager
    {
        [SerializeField] private PlayerInventoryUI playerInventoryUI;
        [SerializeField] private ShopUI shopUI;
        [SerializeField] private int inventoryMoney;

        private Dictionary<ItemSO, int> inventory = new Dictionary<ItemSO, int>();

        public event Action<int> OnMoneyChanged;
        public event Action<ItemSO, int> OnInventoryItemAdded;
        public event Action<ItemSO, int> OnInventoryItemRemove;

        private void Awake()
        {
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

            OnInventoryItemAdded?.Invoke(item, inventory[item]);
        }

        public void RemoveItem(ItemSO item, int quantity)
        {
            if (inventory.ContainsKey(item))
            {
                inventory[item] -= quantity;
                if (inventory[item] <= 0)
                    inventory.Remove(item);

                OnInventoryItemRemove?.Invoke(item, inventory[item]);
            }
        }

        public void UpdateUI()
        {
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
            return inventoryMoney;
        }

        public void SetMoney(int newAmount)
        {
            inventoryMoney = newAmount;
            OnMoneyChanged?.Invoke(inventoryMoney);
        }
    }
}
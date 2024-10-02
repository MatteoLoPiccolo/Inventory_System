using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopList", menuName = "ScriptableObjects/ShopSOList")]
public class ShopSO : ScriptableObject
{
    [SerializeField] private List<Items> shopItems;

    public void Initialize(int size)
    {
        if (shopItems == null || shopItems.Count == 0)
            shopItems = new List<Items>(size);

        for (int i = shopItems.Count; i < size; i++)
            shopItems.Add(Items.GetEmptyItem());
    }

    public Dictionary<int, Items> GetCurrentShopItemState()
    {
        Dictionary<int, Items> shopItemState = new Dictionary<int, Items>();

        for (int i = 0; i < shopItems.Count; i++)
            if (!shopItems[i].IsEmpty)
                shopItemState[i] = shopItems[i];

        return shopItemState;
    }

    public Items GetItemAt(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= shopItems.Count)
            throw new IndexOutOfRangeException();

        return shopItems[itemIndex];
    }
}

[Serializable]
public class Items
{
    [SerializeField] private int quantity;
    [SerializeField] private ItemSO item;

    public event Action<int> OnQuantityChanged;

    public int Quantity => quantity;

    public ItemSO Item => item;

    public bool IsEmpty => Item == null;

    public Items(ItemSO item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void ChangeQuantity()
    {
        quantity --;

        OnQuantityChanged?.Invoke(quantity);
    }

    public static Items GetEmptyItem() => new Items(null, 0);
}
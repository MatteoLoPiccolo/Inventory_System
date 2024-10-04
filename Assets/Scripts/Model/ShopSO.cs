using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(fileName = "ShopList", menuName = "ScriptableObjects/ShopSOList")]
    public class ShopSO : ScriptableObject
    {
        #region Variables

        [SerializeField] private List<Items> shopItems;

        #endregion

        #region Functions

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

        #endregion
    }

    [Serializable]
    public class Items
    {
        #region Variables

        [SerializeField] private int quantity;
        [SerializeField] private ItemSO item;

        public event Action<int> OnQuantityChanged;

        #endregion

        #region Properties

        public int Quantity => quantity;

        public ItemSO Item => item;

        public bool IsEmpty => Item == null;

        #endregion

        #region Functions

        public Items(ItemSO item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }

        public void ChangeQuantity()
        {
            quantity--;
            OnQuantityChanged?.Invoke(quantity);
        }

        public static Items GetEmptyItem() => new(null, 0);

        #endregion
    }
}
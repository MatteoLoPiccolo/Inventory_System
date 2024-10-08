using InventorySystemInterfaces;
using System;

namespace InventorySystemController
{
    public abstract class BaseInventoryController : IMoneyManager
    {
        protected int money;
        public event Action<int> OnMoneyChanged;

        public int GetMoney() => money;

        public void SetMoney(int value)
        {
            money = value;
            OnMoneyChanged?.Invoke(money);
        }

        protected abstract void SetUpUI();

        protected abstract void OnDescriptionRequested(int index);
    }
}
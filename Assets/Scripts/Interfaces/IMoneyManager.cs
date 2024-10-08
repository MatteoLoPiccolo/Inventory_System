using System;

namespace InventorySystemInterfaces
{
    public interface IMoneyManager
    {
        public int GetMoney();

        public void SetMoney(int amount);

        public event Action<int> OnMoneyChanged;
    }
}
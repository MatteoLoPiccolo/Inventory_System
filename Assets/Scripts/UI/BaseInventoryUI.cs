using Controller;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public abstract class BaseInventoryUI<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Variables

        [SerializeField] protected PurchasePopupUI activePurchasePopupUIInstance;
        [SerializeField] protected SellPopupUI activeSellPopupUIInstance;
        [SerializeField] protected UiItemDescription itemDescription;
        [SerializeField] protected RectTransform contentRectTransform;
        [SerializeField] protected TMP_Text moneyText;

        protected List<T> uiShopItems = new List<T>();
        protected List<T> uiInventoryItems = new List<T>();

        public event Action<int> OnDescriptionRequested;

        #endregion

        #region Properties

        public PurchasePopupUI ActivePurchasePopupUIInstance
        {
            get
            {
                if (activePurchasePopupUIInstance == null)
                {
                    activePurchasePopupUIInstance = Instantiate(activePurchasePopupUIInstance, transform);
                    activePurchasePopupUIInstance.gameObject.SetActive(false);
                }

                return activePurchasePopupUIInstance;
            }
        }

        public SellPopupUI ActiveSellPopupUIInstance
        {
            get
            {
                if (activeSellPopupUIInstance == null)
                {
                    activeSellPopupUIInstance = Instantiate(activeSellPopupUIInstance, transform);
                    activeSellPopupUIInstance.gameObject.SetActive(false);
                }

                return activeSellPopupUIInstance;
            }
        }

        #endregion

        #region Obj life cycle

        protected virtual void Awake()
        {
            if (activePurchasePopupUIInstance == null)
            {
                Debug.LogError("PurchasePopupInstance is null!");
                return;
            }

            if (activeSellPopupUIInstance == null)
            {
                Debug.LogError("SellPopupInstance is null!");
                return;
            }
        }

        #endregion

        #region Functions

        protected abstract void OnLeftClick(T itemUI);

        protected abstract void OnRightClick(T itemUI);

        public void ClosePurchasePopup()
        {
            ActivePurchasePopupUIInstance.gameObject.SetActive(false);
            activePurchasePopupUIInstance.OnPurchaseConfirmed -= GameManager.Instance.ShopController.OnPurchaseConfirmed;
        }

        public void CloseSellPopup()
        {
            ActiveSellPopupUIInstance.gameObject.SetActive(false);
            activeSellPopupUIInstance.OnSellConfirmed -= GameManager.Instance.InventoryController.OnSellConfirmed;
        }

        public virtual void Clear()
        {
            foreach (Transform child in contentRectTransform)
                Destroy(child.gameObject);
            uiShopItems.Clear();
        }

        public List<T> GetUIShopItems() { return uiShopItems; }

        protected void InvokeDescriptionRequested(int index)
        {
            OnDescriptionRequested?.Invoke(index);
        }

        #endregion
    }
}
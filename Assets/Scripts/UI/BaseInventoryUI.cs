using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BaseInventoryUI<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected PurchasePopupUI activePopupUIInstance;
    [SerializeField] protected UiItemDescription itemDescription;
    [SerializeField] protected RectTransform contentRectTransform;
    [SerializeField] protected TMP_Text moneyText;

    protected List<T> uiItems = new List<T>();

    public event Action<int> OnDescriptionRequested;
    public event Action<int> OnItemActionRequested;

    public PurchasePopupUI ActivePopupUIInstance
    {
        get
        {
            if (activePopupUIInstance == null)
            {
                activePopupUIInstance = Instantiate(activePopupUIInstance, transform);
                activePopupUIInstance.gameObject.SetActive(false);
            }

            return activePopupUIInstance;
        }
    }

    protected virtual void Awake()
    {
        if (activePopupUIInstance == null)
        {
            Debug.LogError("PurchasePopupInstance is null!");
            return;
        }

        activePopupUIInstance.gameObject.SetActive(false);
    }

    protected abstract void InitializeUIList(int listSize);

    protected abstract void OnLeftClick(T itemUI);

    protected abstract void OnRightClick(T itemUI);

    public void ClosePopup()
    {
        ActivePopupUIInstance.gameObject.SetActive(false);
        activePopupUIInstance.OnPurchaseConfirmed -= GameManager.Instance.GetShopController().OnPurchaseConfirmed;
    }

    public virtual void Clear()
    {
        foreach (Transform child in contentRectTransform)
            Destroy(child.gameObject);
        uiItems.Clear();
    }

    protected void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description, int price)
    {
        if (itemIndex < 0 || itemIndex >= uiItems.Count)
            return;

        itemDescription.SetDescription(itemImage, name, description, price);
    }


    protected void InvokeDescriptionRequested(int index)
    {
        OnDescriptionRequested?.Invoke(index);
    }


}
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BaseInventoryUI<T> : MonoBehaviour  where T : MonoBehaviour
{
    [SerializeField] protected UiItemDescription itemDescription;
    [SerializeField] protected RectTransform contentRectTransform;
    [SerializeField] protected TMP_Text moneyText;

    protected PurchasePopupUI activePopupInstance;

    protected List<T> uiItems = new List<T>();

    public event Action<int> OnDescriptionRequested;
    public event Action<int> OnItemActionRequested;

    protected virtual void Awake() {}

    protected abstract void InitializeUIList(int listSize);

    protected abstract void OnLeftClick(T itemUI);

    protected abstract void OnRightClick(T itemUI);

    public void ClosePopup()
    {
        if (activePopupInstance != null)
        {
            Destroy(activePopupInstance.gameObject);
            activePopupInstance = null;
        }
    }

    protected void AddItemUI(T itemUI)
    {
        itemUI.transform.SetParent(contentRectTransform);
        uiItems.Add(itemUI);
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

    public void UpdateMoneyText(int money)
    {
        if (moneyText)
            moneyText.text = $"Money: {money}";
    }

    protected void InvokeDescriptionRequested(int index)
    {
        OnDescriptionRequested?.Invoke(index);
    }

    protected void InvokeItemActionRequested(int index)
    {
        OnItemActionRequested?.Invoke(index);
    }
}

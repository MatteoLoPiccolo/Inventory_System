using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BaseInventoryUI<T> : MonoBehaviour  where T : MonoBehaviour
{
    [SerializeField] protected RectTransform contentRectTransform;
    [SerializeField] protected TMP_Text moneyText;

    protected List<T> uiItems = new List<T>();

    public event Action<int> OnDescriptionRequested, OnItemActionRequested;

    protected virtual void Awake() {}

    protected abstract void InitializeUIList(int listSize);

    protected abstract void OnLeftClick(T itemUI);

    protected abstract void OnRightClick(T itemUI);

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

    public virtual void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description, int price)
    {
        if (itemIndex < 0 || itemIndex >= uiItems.Count)
            return;
        // Implementa la logica per aggiornare la descrizione
    }

    // Metodo per aggiornare il denaro
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

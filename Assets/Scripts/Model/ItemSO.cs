using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] [field: TextArea] private string description;
    [SerializeField] private Sprite itemImage;
    [SerializeField] private int price;

    public string ItemName => itemName;
    public string Description => description;
    public Sprite ItemImage => itemImage;
    public int Price => price;
}
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/Item")]
    public class ItemSO : ScriptableObject
    {
        #region Variables

        [SerializeField] private string itemName;
        [SerializeField][field: TextArea] private string description;
        [SerializeField] private Sprite itemImage;
        [SerializeField] private int price;

        #endregion

        #region Properties

        public string ItemName => itemName;
        public string Description => description;
        public Sprite ItemImage => itemImage;
        public int Price => price;

        #endregion
    }
}
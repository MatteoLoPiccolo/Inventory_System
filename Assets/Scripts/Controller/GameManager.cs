using InventorySystemModel;
using InventorySystemPlayer;
using InventorySystemUI;
using UnityEngine;

namespace InventorySystemController
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ShopUI shopUI;
        [SerializeField] private ShopSO shopData;
        [SerializeField] private PlayerInventoryUI playerInventoryUI;
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private UIController uiController;
        [SerializeField] private PurchasePopupUI purchasePopupUI;
        [SerializeField] private int listCount = 7;
        [SerializeField] private int shopMoney = 1000;
        [SerializeField] private int inventoryMoney = 100;

        private ShopController shopController;
        private InventoryController inventoryController;
        private InputManager inputManager;

        private static GameManager instance;

        public static GameManager Instance { get { return instance; } }

        public ShopController ShopController { get { return shopController; } }

        public PlayerInventory PlayerInventory { get { return playerInventory; } }

        public InventoryController InventoryController { get { return inventoryController; } }

        public ShopUI ShopUI { get { return shopUI; } }

        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(gameObject);
            else
                instance = this;

            inventoryController = new InventoryController(playerInventoryUI, playerInventory, shopData, shopUI);
            inputManager = new InputManager();

            
        }

        private void Start()
        {
            if (uiController == null)
                Debug.LogError("UIController is null in GameManager!");
            else
                inputManager.OnInventoryTogglePressed += uiController.ToggleShop;

            shopController = new ShopController(shopUI, shopData, uiController, playerInventory, listCount);
            shopController.SetMoney(shopMoney);

            inventoryController.SetMoney(inventoryMoney);

        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.I))
                inputManager.HandleInput();
        }
    }
}
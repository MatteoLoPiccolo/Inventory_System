using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private ShopSO shopData;
    [SerializeField] private PlayerInventoryUI playerInventoryUI;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private UIController uiController;

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

    internal InventoryController InventoryController { get { return inventoryController; } }

    public event Action OnSwitchUI;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        inventoryController = new InventoryController(playerInventoryUI, playerInventory, shopData, inventoryMoney);
        inputManager = new InputManager();

        shopMoney = 1000;
        inventoryMoney = 100;
    }

    private void Start()
    {
        Debug.Log("GameManager Start");
        if (uiController == null)
        {
            Debug.LogError("UIController is null in GameManager!");
        }
        else
        {
            inputManager.OnInventoryTogglePressed += uiController.ToggleShop;
        }

        shopController = new ShopController(shopUI, shopData, uiController, playerInventory, listCount, shopMoney);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            inputManager.HandleInput();
        }
    }

    
}
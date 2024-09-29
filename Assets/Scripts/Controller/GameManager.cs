using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private ShopSO shopData;
    [SerializeField] private UIController uiController;
    [SerializeField] private int listCount = 7;
    [SerializeField] private int shopMoney = 1000;
    
    private ShopController shopController;
    private InputManager inputManager;
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

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

        shopController = new ShopController(shopUI, shopData, uiController, listCount, shopMoney);
        inputManager = new InputManager();

        shopMoney = 1000;
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
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            Debug.Log("Inventory Toggle Pressed");
            inputManager.HandleInput();
        }
    }

    public void SwitchUI()
    {
        OnSwitchUI?.Invoke();
        // Logica per attivare lo Shop o Inventory UI in base allo stato corrente
    }

    public ShopController GetShopController() { return shopController; }

    public InputManager GetInputManager() { return inputManager; }
}
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private ShopSO shopData;
    [SerializeField] private UIController uiController;

    private ShopController shopController;
    private InputManager inputManager;

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

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

        shopController = new ShopController(shopUI, shopData, uiController, 7, 1000);
        inputManager = new InputManager();
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

    public ShopController GetShopController() { return shopController; }

    public InputManager GetInputManager() { return inputManager; }
}
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private ShopSO shopData;
    [SerializeField] private UIController uiManager;

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

        inputManager = new InputManager();
        shopController = new ShopController(shopUI, shopData, uiManager, 7, 1000);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            uiManager.ToggleShop();
        }
    }

    public ShopController GetShopController() { return shopController; }

    public InputManager GetInputManager() { return inputManager; }
}
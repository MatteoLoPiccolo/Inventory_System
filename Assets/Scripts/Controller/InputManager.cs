using System;
using UnityEngine;

public class InputManager
{
    public event Action OnInventoryTogglePressed;

    public void HandleInput()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            OnInventoryTogglePressed?.Invoke();
        }
    }
}
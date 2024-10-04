using System;
using UnityEngine;

namespace Controller
{
    public class InputManager
    {
        public event Action OnInventoryTogglePressed;

        public void HandleInput()
        {
            if (Input.GetKeyUp(KeyCode.I))
                OnInventoryTogglePressed?.Invoke();
        }
    }
}
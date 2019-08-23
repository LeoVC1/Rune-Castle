using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public InputManager inputManager;

    private void Update()
    {
        if (Input.GetKeyDown(inputManager.interactInput))
            inputManager.Interact();

        if (Input.GetKeyDown(inputManager.changeClassInput))
            inputManager.ChangeClass();
    }
}

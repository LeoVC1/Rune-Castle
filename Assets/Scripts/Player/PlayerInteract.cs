using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public InputManager inputManager;

    private void Update()
    {
        if (Input.GetKeyDown(inputManager.interactInput))
            inputManager.Interact();
    }
}

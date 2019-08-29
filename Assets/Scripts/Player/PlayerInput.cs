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

        if (Input.GetKeyDown(inputManager.confirmInput))
            inputManager.Confirm();

        if (Input.GetKeyDown(inputManager.skill1))
            inputManager.Skill1();

        if (Input.GetKeyDown(inputManager.skill2))
            inputManager.Skill2();
    }
}

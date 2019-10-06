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

        if (Input.GetKeyDown(inputManager.confirmInput[0]) || Input.GetKeyDown(inputManager.confirmInput[1]))
            inputManager.Confirm();

        if (inputManager.isCastingSpell)
            return;

        if (Input.GetKeyDown(inputManager.skill1))
            inputManager.Skill1();

        if (Input.GetKeyDown(inputManager.skill2))
            inputManager.Skill2();

        if (Input.GetKeyDown(inputManager.item2))
            inputManager.Item2();
    }
}

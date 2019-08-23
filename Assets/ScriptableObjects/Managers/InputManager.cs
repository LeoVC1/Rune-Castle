using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputManager", menuName = "Scriptable Objects/Managers/Input Manager")]
public class InputManager : ScriptableObject
{
    public GameManager gameManager;

    public bool isMovementLocked;

    public KeyCode interactInput;

    public KeyCode changeClassInput;

    public GameEvent interactEvent;

    public void LockMovement()
    {
        isMovementLocked = true;
    }

    public void UnlockMovement()
    {
        isMovementLocked = false;
    }

    public void Interact()
    {
        interactEvent.Raise();
    }

    public void ChangeClass()
    {
        gameManager.ChangeCharacter();
    }
}

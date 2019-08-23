using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputManager", menuName = "Scriptable Objects/Managers/Input Manager")]
public class InputManager : ScriptableObject
{
    public bool isMovementLocked;

    public KeyCode interactInput;

    public GameEvent inputEvent;

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
        inputEvent.Raise();
    }
}

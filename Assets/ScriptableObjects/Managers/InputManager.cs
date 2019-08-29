using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputManager", menuName = "Scriptable Objects/Managers/Input Manager")]
public class InputManager : ScriptableObject
{
    public GameManager gameManager;

    [HideInInspector] public bool isMovementLocked;

    public KeyCode interactInput;
    public KeyCode confirmInput;
    public KeyCode changeClassInput;

    [Space]
    public KeyCode skill1;
    public KeyCode skill2;

    [Space]
    public GameEvent interactEvent;
    public GameEvent confirmEvent;
    public GameEvent skill1Event;
    public GameEvent skill2Event;

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

    public void Confirm()
    {
        confirmEvent.Raise();
    }

    public void ChangeClass()
    {
        gameManager.ChangeCharacter();
    }

    public void Skill1()
    {
        Debug.Log("Skill 1");
        skill1Event.Raise();
    }

    public void Skill2()
    {
        Debug.Log("Skill 2");
        skill2Event.Raise();
    }
}

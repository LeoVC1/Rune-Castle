using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputManager", menuName = "Scriptable Objects/Managers/Input Manager")]
public class InputManager : ScriptableObject
{
    public GameManager gameManager;

    public bool isMovementLocked;
    public bool isCameraFreezed;
    public bool isWaitingConfirmEvent;
    public bool isCastingSpell;
    public bool canAttack;

    public KeyCode interactInput;
    public KeyCode[] confirmInput;
    public KeyCode changeClassInput;
    public KeyCode sprintInput;

    [Space]
    public KeyCode skill1;
    public KeyCode skill2;

    [Space]
    public KeyCode item1;
    public KeyCode item2;

    [Space]
    public GameEvent interactEvent;
    public GameEvent confirmEvent;
    public GameEvent skill1Event;
    public GameEvent skill2Event;
    public GameEvent item2Event;
    public GameEvent onLockMovement;
    public GameEvent onUnlockMovement;

    public void LockMovement()
    {
        isMovementLocked = true;
        onLockMovement.Raise();
    }

    public void UnlockMovement()
    {
        isMovementLocked = false;
    }

    public void FreezeCamera()
    {
        isCameraFreezed = true;
    }

    public void UnfreezeCamera()
    {
        isCameraFreezed = false;
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
        if(!isMovementLocked)
            gameManager.ChangeCharacter();
    }

    public void Skill1()
    {
        if (!canAttack)
            return;

        skill1Event.Raise();
    }

    public void Skill2()
    {
        if (!canAttack)
            return;

        skill2Event.Raise();
    }

    public void Item2()
    {
        if (!canAttack)
            return;

        skill2Event.Raise();
    }
}

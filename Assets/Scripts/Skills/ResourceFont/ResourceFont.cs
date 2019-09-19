using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFont : Interactable
{
    public InventoryManager inventoryManager;
    public InputManager inputManager;

    [Space]
    public Item item;
    public bool removeItem;
    public Item itemToRemove;

    [Space]
    public bool delay;
    public float delayTime;
    public FloatVariable delayTimer;
    public bool onDelay;


    public void GetResource()
    {
        if (onDelay)
            return;

        if (removeItem)
        {
            if (inventoryManager.CheckItemAcquirement(itemToRemove) > 0 && inventoryManager.CanAddItem(item))
            {
                onDelay = true;
                inputManager.canAttack = false;
                StartCoroutine(Delay());
            }

        }
        else
        {
            UpdateInventory();
        }
    }

    IEnumerator Delay()
    {
        inputManager.LockMovement();
        inputManager.FreezeCamera();

        delayTimer.ConstantValue = delayTime;

        while(delayTimer.Value <= delayTimer.ConstantValue)
        {
            delayTimer.Value += Time.deltaTime;
            yield return null;
        }

        UpdateInventory();

        delayTimer.Value = 0;

        inputManager.canAttack = true;
        onDelay = false;
        inputManager.UnlockMovement();
        inputManager.UnfreezeCamera();
    }

    public void UpdateInventory()
    {
        if (removeItem && inventoryManager.CheckItemAcquirement(itemToRemove) > 0)
        {
            inventoryManager.AddItem(item);
            inventoryManager.RemoveItem(itemToRemove);
        }
        else
        {
            inventoryManager.AddItem(item);
        }
    }
}

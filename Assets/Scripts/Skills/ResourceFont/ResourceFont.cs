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


    public void GetResource()
    {
        if (inventoryManager.CanAddItem(item))
        {
            if (delay)
            {
                StartCoroutine(Delay());
            }
            else
            {
                UpdateInventory();
            }
        }
    }

    IEnumerator Delay()
    {
        inputManager.LockMovement();

        float t = 0;

        delayTimer.ConstantValue = delayTime;


        while(t <= 1)
        {

            delayTimer.Value = Mathf.Lerp(0, delayTime, t);
            t += Time.deltaTime / delayTime;

            yield return new WaitForEndOfFrame();
        }

        UpdateInventory();

        delayTimer.ConstantValue = delayTime;
        delayTimer.Value = 0;

        inputManager.UnlockMovement();
    }

    public void UpdateInventory()
    {
        if (removeItem)
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

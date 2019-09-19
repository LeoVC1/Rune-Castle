using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [Header("Managers:")]
    public InventoryManager inventoryManager;
    public InputManager inputManager;

    [Header("References:")]
    public Item itemResource;
    public FloatVariable resource;

    [Header("Properties:")]
    public float generateAmount;

    private void Update()
    {
        DrinkPotion();
    }

    public void DrinkPotion()
    {
        if (inputManager.isMovementLocked || inputManager.isCastingSpell)
            return;

        if (Input.GetKeyDown(inputManager.item1))
        {
            if (inventoryManager.CheckItemAcquirement(itemResource) > 0)
            {
                inventoryManager.RemoveItem(itemResource);
                resource.Value += generateAmount;
            }
        }
    }
}

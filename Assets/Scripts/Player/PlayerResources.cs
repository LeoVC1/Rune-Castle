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
    public IntVariable activeRune;
    public GameObject[] runesPrefab;

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

    public void DropRune()
    {
        if (activeRune.Value != -1)
        {
            GameObject rune = Instantiate(runesPrefab[activeRune.Value], transform.position + Vector3.up * 5, Quaternion.identity);
            Rigidbody rb = rune.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 300);
        }
    }
}

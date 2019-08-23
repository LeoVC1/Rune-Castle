using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterResource", menuName = "Scriptable Objects/Character/New Resource")]
public class ResourceReference : ScriptableObject
{
    public Character myClass;
    public bool isItem;
    public Item ItemReference;
    public FloatVariable FloatReference;
    public InventoryManager inventoryManager;

    public float Value
    {
        get
        {
            return isItem ? inventoryManager.CheckItemAcquirement(ItemReference) : FloatReference.Value;
        }
    }
}

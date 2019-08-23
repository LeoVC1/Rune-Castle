using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryManager", menuName = "Scriptable Objects/Managers/Inventory Manager")]
public class InventoryManager : ScriptableObject
{
    public List<Item> itens;

    public void AddItem(Item item)
    {
        int itemCount = CheckItemAcquirement(item);
        if ((item.canCarryMultiple && itemCount < item.maximumCount)|| itemCount == 0)
        {
            itens.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        itens.Remove(item);
    }

    public bool CanAddItem(Item item)
    {
        int itemCount = CheckItemAcquirement(item);
        if ((item.canCarryMultiple && itemCount < item.maximumCount) || itemCount == 0)
        {
            return true;
        }
        return false;
    }

    public int CheckItemAcquirement(Item item)
    {
        int count = 0;
        foreach(Item i in itens)
        {
            if (i.name == item.name)
                count++;
        }
        return count;
    }
}
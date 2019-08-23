using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    public InventoryManager inventoryManager;
    [Space]
    public TextMeshProUGUI text;
    public Item item;

    public void Update()
    {
        text.text = "x" + inventoryManager.CheckItemAcquirement(item).ToString();
    }
}

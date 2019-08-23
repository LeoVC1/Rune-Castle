using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Objects/Variables/Item")]
public class Item : ScriptableObject
{
    public bool canCarryMultiple;
    public int maximumCount;
}
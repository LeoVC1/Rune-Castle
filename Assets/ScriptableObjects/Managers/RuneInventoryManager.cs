using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunesManager", menuName = "Scriptable Objects/Managers/Runes Manager")]
public class RuneInventoryManager : ScriptableObject
{
    public int[] runes = new int[5];

    public GameEvent onGetNewRune;

    public RuneSequence[] allSequences;

    public List<RuneSequence> possibleCombos;
    public RuneSequence activeCombo;

    public void Initialize()
    {
        possibleCombos.Clear();
        activeCombo = new RuneSequence();
        runes = new int[5];
    }

    public void AddRune(int runeID)
    {
        runes[runeID]++;
        OnGetNewRune(runeID);
    }

    public bool CanGetRune(int runeID)
    {
        return true;
    }

    private void OnGetNewRune(int runeID)
    {
        onGetNewRune.Raise();
    }
}

[System.Serializable]
public struct RuneSequence
{
    public string name;
    public List<int> combo;
}
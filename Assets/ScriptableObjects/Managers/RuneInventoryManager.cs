using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunesManager", menuName = "Scriptable Objects/Managers/Runes Manager")]
public class RuneInventoryManager : ScriptableObject
{
    [SerializeField] private List<int> runes;

    public GameEvent onGetNewRune;

    public RuneSequence[] allSequences;

    public List<RuneSequence> possibleCombos;
    public RuneSequence activeCombo;

    public void Initialize()
    {
        possibleCombos.Clear();
        activeCombo = new RuneSequence();
        runes.Clear();
    }

    public void AddRune(int runeID)
    {
        runes.Add(runeID);
        OnGetNewRune(runeID);
    }

    public bool CanGetRune(int runeID)
    {
        if (runes.Contains(runeID))
            return false;
        else if (possibleCombos.Count == 0)
            return true;
        else
            foreach (RuneSequence sequence in possibleCombos)
            {
                if (sequence.combo.Contains(runeID))
                {
                    return true;
                }
            }

        return false;
    }

    private void OnGetNewRune(int runeID)
    {
        if (possibleCombos.Count == 0)
            foreach (RuneSequence sequence in allSequences)
            {
                if (sequence.combo.Contains(runeID))
                    possibleCombos.Add(sequence);
            }
        else
            for (int i = possibleCombos.Count - 1; i >= 0; i--)
            {
                if (!possibleCombos[i].combo.Contains(runeID))
                {
                    possibleCombos.Remove(possibleCombos[i]);
                }
            }

        SetActiveCombo();

        //onGetNewRune.Raise();
    }

    private void SetActiveCombo()
    {
        for (int i = possibleCombos.Count - 1; i >= 0; i--)
        {
            int runeMatch = 0;
            for (int j = possibleCombos[i].combo.Count - 1; j >= 0; j--)
            {
                if (runes.Contains(possibleCombos[i].combo[j]))
                {
                    runeMatch++;
                }
            }
            if (runeMatch == possibleCombos[i].combo.Count - 1)
            {
                activeCombo = possibleCombos[i];
                break;
            }
        }
    }
}

[System.Serializable]
public struct RuneSequence
{
    public string name;
    public List<int> combo;
}
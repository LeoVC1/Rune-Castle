using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunesManager", menuName = "Scriptable Objects/Managers/Runes Manager")]
public class RuneInventoryManager : ScriptableObject
{
    public int[] runes = new int[5];

    public List<int> activeRunes = new List<int>();

    public GameEvent onGetNewRune;

    public RuneSequence[] allSequences;

    public List<RuneSequence> possibleCombos;
    public RuneSequence activeCombo;

    public void Initialize()
    {
        possibleCombos.Clear();
        activeCombo = new RuneSequence();
        activeRunes.Clear();
        //runes = new int[5];
    }

    public void AddRune(int runeID)
    {
        runes[runeID]++;
        onGetNewRune.Raise();
    }

    public void RemoveRune(int runeID)
    {
        runes[runeID]--;
        onGetNewRune.Raise();
    }

    public void AddRuneToCombo(int runeID)
    {
        activeRunes.Add(runeID);
        UpdateCombo();
    }

    public void RemoveRuneFromCombo(int runeID)
    {
        activeRunes.Remove(runeID);
        UpdateCombo();
    }

    public bool CanGetRune(int runeID)
    {
        return true;
    }

    private void UpdateCombo()
    {
        CheckRunePresence();

        SetActiveCombo();

        //onGetNewRune.Raise();
    }

    public void ConsumeRune()
    {
        foreach(int rune in activeRunes)
        {
            runes[rune]--;
        }
    }

    private void CheckRunePresence()
    {
        foreach(RuneSequence sequence in allSequences)
        {
            foreach (int rune in activeRunes)
            {
                if (sequence.combo.Contains(rune) && !possibleCombos.Contains(sequence))
                    possibleCombos.Add(sequence);
            }
        }

        for (int i = possibleCombos.Count - 1; i >= 0; i--)
        {
            int k = 0;
            for(int j = 0; j < activeRunes.Count; j++)
            {
                if (possibleCombos[i].combo.Contains(activeRunes[j]))
                {
                    k++;
                }
            }
            if(!(k == activeRunes.Count))
            {
                possibleCombos.Remove(possibleCombos[i]);
            }
        }
    }

    private void SetActiveCombo()
    {
        if(possibleCombos.Count == 0)
            activeCombo = new RuneSequence();
        else
        {
            for (int i = possibleCombos.Count - 1; i >= 0; i--)
            {
                int runeMatch = 0;
                for (int j = possibleCombos[i].combo.Count - 1; j >= 0; j--)
                {
                    if (activeRunes.Contains(possibleCombos[i].combo[j]))
                    {
                        runeMatch++;
                    }
                }
                Debug.Log(runeMatch);
                if (runeMatch == possibleCombos[i].combo.Count)
                {
                    activeCombo = possibleCombos[i];
                    Debug.Log(activeRunes);
                    break;
                }
                else
                {
                    activeCombo = new RuneSequence();
                }
            }
        }
    }
}

[System.Serializable]
public struct RuneSequence
{
    public string name;
    public List<int> combo;
    public GameEvent comboEvent;
}
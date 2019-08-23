using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "Scriptable Objects/Managers/Game Manager")]
public class GameManager : ScriptableObject
{
    public Character characterClass;
    public GameEvent OnChangeClass;

    public void ChangeCharacter()
    {
        characterClass = characterClass == Character.MAGE ? Character.ENGINNEER : Character.MAGE;
        OnChangeClass.Raise();
    }
}

public enum Character { MAGE, ENGINNEER, BOTH }


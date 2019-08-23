using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "Scriptable Objects/Managers/Game Manager")]
public class GameManager : ScriptableObject
{
    public Character character;

    public void ChangeCharacter()
    {
        character = character == Character.MAGE ? Character.ENGINNEER : Character.MAGE;
    }
}

public enum Character { MAGE, ENGINNEER }


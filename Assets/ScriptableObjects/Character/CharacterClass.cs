using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterClass", menuName = "Scriptable Objects/Character/New Class")]
public class CharacterClass : ScriptableObject
{
    [Header("Images:")]
    public Sprite iconImage;
    public Sprite resourceImage;
    public Sprite resourceBarImage;
}

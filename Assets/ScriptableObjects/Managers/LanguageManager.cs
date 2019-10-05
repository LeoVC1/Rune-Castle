using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Language Manager", menuName = "Scriptable Objects/Managers/Language Manager")]
public class LanguageManager : ScriptableObject
{
    public GameEvent onChangeLanguage;
    public Languages activeLanguage;

    public void ChangeLanguage(Languages thisLanguage)
    {
        activeLanguage = thisLanguage;
        onChangeLanguage.Raise();
    }
}

public enum Languages
{
    PORTUGUESE,
    ENGLISH,
}

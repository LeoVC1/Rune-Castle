using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(GameEventListener))]
public class TextElement : MonoBehaviour
{
    public LanguageManager languageManager;
    [TextArea(0, 4)]
    public string englishVersion;
    [TextArea(0, 4)]
    public string portugueseVersion;

    public TextMeshProUGUI textComponent;
    public GameEventListener listener;

    private void Awake()
    {
        listener.Response.AddListener(OnChangeLanguage);
    }

    private void Start()
    {
        //if (!languageManager)
        //{
        //    languageManager = Resources.Load("Managers/Language Manager") as LanguageManager;
        //}
        //if (!textComponent)
        //{
        //    textComponent = GetComponent<TextMeshProUGUI>();
        //}
        //if (!listener)
        //{
        //    gameObject.AddComponent(typeof(GameEventListener));
        //    listener = GetComponent<GameEventListener>();
        //    listener.Response.AddListener(OnChangeLanguage);
        //    listener.Event = Resources.Load("Events/OnChangeLanguage") as GameEvent;
        //}
        listener.Response.AddListener(OnChangeLanguage);
        OnChangeLanguage();
    }

    private void OnEnable()
    {
        OnChangeLanguage();
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (!languageManager)
        {
            languageManager = Resources.Load("Managers/Language Manager") as LanguageManager;
        }
        if (!textComponent)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }
        if (!listener)
        {
            //listener = GetComponent<GameEventListener>();
            //if (listener == null)
            //    gameObject.AddComponent(typeof(GameEventListener));
            listener = GetComponent<GameEventListener>();
            listener.Event = Resources.Load("Events/OnChangeLanguage") as GameEvent;
        }
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public void OnChangeLanguage()
    {
        switch (languageManager.activeLanguage)
        {
            case Languages.ENGLISH:
                textComponent.text = englishVersion;
                break;
            case Languages.PORTUGUESE:
                textComponent.text = portugueseVersion;
                break;
        }
    }
}

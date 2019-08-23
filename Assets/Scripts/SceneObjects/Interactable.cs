using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class Interactable : MonoBehaviour
{
    public GameManager gameManager;
    public Character intectableClasses;
    GameEventListener listener;

    private void Start()
    {
        listener = GetComponent<GameEventListener>();
        listener.enabled = false;
    }

    public void InteractionResponseTest()
    {
        Debug.Log(name + " is responding with input event.");
    }

    public virtual void TriggerEnter() { }

    public virtual void TriggerExit() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            if(gameManager.characterClass == intectableClasses || intectableClasses == Character.BOTH)
                listener.enabled = true;
        TriggerEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            if (gameManager.characterClass == intectableClasses || intectableClasses == Character.BOTH)
                listener.enabled = false;
        TriggerExit();
    }
}
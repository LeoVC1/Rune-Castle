﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class Interactable : MonoBehaviour
{
    [Header("(Interactable) Managers:")]
    public GameManager gameManager;
    public InteractableManager interactableManager;
    public Character intectableClasses;

    GameEventListener listener;
    public GameObject canvas;

    public virtual void Start()
    {
        listener = GetComponent<GameEventListener>();
        listener.enabled = false;
        canvas = GetComponentInChildren<Canvas>(true).gameObject;
    }

    public void InteractionResponseTest()
    {
        Debug.Log(name + " is responding input event.");
    }

    public virtual void ActivateInteraction()
    {
        listener.enabled = true;
        canvas.SetActive(true);
    }

    public virtual void DesactivateInteraction()
    {
        listener.enabled = false;
        canvas.SetActive(false);
    }

    public virtual void TriggerEnter() { }

    public virtual void TriggerExit() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            if (gameManager.characterClass == intectableClasses || intectableClasses == Character.BOTH)
                interactableManager.RegisterInteractable(this);
        TriggerEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            if (gameManager.characterClass == intectableClasses || intectableClasses == Character.BOTH)
                interactableManager.UnregisterInteractable(this);
        TriggerExit();
    }
}
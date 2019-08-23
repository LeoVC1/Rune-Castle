using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    public GameManager gameManager;

    public Character myClass;

    PlayerInput playerInput;
    PlayerMovement playerMovement;
    DelayBar delayBar;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        delayBar = GetComponentInChildren<DelayBar>();
        OnChangeClass();
    }

    public void OnChangeClass()
    {
        if(gameManager.characterClass == myClass)
        {
            playerInput.enabled = true;
            playerMovement.enabled = true;
            delayBar.Show();
        }
        else
        {
            playerInput.enabled = false;
            playerMovement.enabled = false;
            delayBar.Hide();
        }
    }
}

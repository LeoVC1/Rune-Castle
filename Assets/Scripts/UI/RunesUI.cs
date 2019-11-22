using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunesUI : MonoBehaviour
{
    public RuneInventoryManager runeInventoryManager;
    public InputManager inputManager;

    public TextMeshProUGUI[] runesNumbers;
    public Button[] runesButtons;

    public CanvasGroup canvas;

    void Start()
    {
        DisableCanvas();
        UpdateNumbers();
    }

    void Update()
    {
        if (inputManager.onCutscene)
        {
            DisableCanvas();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EnableCanvas();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            DisableCanvas();
        }
    }

    public void UpdateNumbers()
    {
        for(int i = 0; i < runesNumbers.Length; i++)
        {
            runesNumbers[i].text = runeInventoryManager.runes[i].ToString();
            if (runeInventoryManager.runes[i] <= 0)
                runesButtons[i].interactable = false;
            else
                runesButtons[i].interactable = true;
        }
    }

    public void EnableCanvas()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        inputManager.canAttack = false;
        inputManager.FreezeCamera();
        inputManager.LockMovement();
    }

    public void DisableCanvas()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inputManager.canAttack = true;
        inputManager.UnfreezeCamera();
        inputManager.UnlockMovement();
    }
}

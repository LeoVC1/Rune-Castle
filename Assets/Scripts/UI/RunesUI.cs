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
    public Image[] slotsImages;
    public Button[] slotsButtons;
    public Sprite[] spritesRunes;
    public Button confirmButton;

    public CanvasGroup canvas;

    void Start()
    {
        DisableCanvas();
        UpdateNumbers();
        VerifyCombo();
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

    private bool VerifySlots()
    {
        int slot = 0;
        for (int i = 0; i < slotsImages.Length; i++)
        {
            if (slotsImages[i].color.a == 1)
            {
                slot++;
            }
        }
        if(slot == 3)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetRune(int runeID)
    {
        if (!VerifySlots())
            return;

        for(int i = 0; i < slotsImages.Length; i++)
        {
            if(slotsImages[i].color.a == 0)
            {
                ChangeSlot(i, runeID, true);
                break;
            }
        }
        runeInventoryManager.RemoveRune(runeID);
        runeInventoryManager.AddRuneToCombo(runeID);
        VerifyCombo();
    }

    public void RemoveRune(int index, int runeID)
    {
        runeInventoryManager.RemoveRuneFromCombo(runeID);
        ChangeSlot(index, runeID);
        VerifyCombo();
    }


    private void VerifyCombo()
    {
        if (runeInventoryManager.activeCombo.comboEvent)
        {
            confirmButton.interactable = true;
            confirmButton.onClick.AddListener(() => ActivateCombo());
        }
        else
        {
            confirmButton.interactable = false;
            confirmButton.onClick.RemoveAllListeners();
        }
    }

    public void ActivateCombo()
    {
        DisableCanvas();
        runeInventoryManager.activeCombo.comboEvent.Raise();
        runeInventoryManager.ConsumeRune();
        foreach(Button btnSlot in slotsButtons)
        {
            btnSlot.onClick.Invoke();
        }
        VerifyCombo();
    }

    
    public void ChangeSlot(int slotIndex, int runeID, bool changeSprite = false)
    {
        if (changeSprite)
        {
            slotsImages[slotIndex].sprite = spritesRunes[runeID];

            Color aux = slotsImages[slotIndex].color;
            aux.a = 1;
            slotsImages[slotIndex].color = aux;

            slotsButtons[slotIndex].interactable = true;
            slotsButtons[slotIndex].onClick.AddListener(() => RemoveRune(slotIndex, runeID));
            slotsButtons[slotIndex].onClick.AddListener(() => runeInventoryManager.AddRune(runeID));
        }
        else
        {
            slotsImages[slotIndex].sprite = null;

            Color aux = slotsImages[slotIndex].color;
            aux.a = 0;
            slotsImages[slotIndex].color = aux;

            slotsButtons[slotIndex].interactable = false;

            slotsButtons[slotIndex].onClick.RemoveAllListeners();
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

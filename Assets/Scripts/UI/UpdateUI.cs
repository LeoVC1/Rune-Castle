using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    public GameManager manager;
    public FloatVariable Iron, Mana;

    private FloatVariable resourceValue;
    public Image resourceBar;

    void Update()
    {
        resourceValue = manager.character == Character.MAGE ? Mana : Iron;
        resourceBar.fillAmount = resourceValue.Value / 100;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceBarUI : ValueReferenceUI
{
    public Image resourceBar;

    public bool grow;

    public FloatVariable mana;

    public float amount;

    public override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (activeResource.Value < 100)
        {
            if (grow)
            {
                mana.Value += Time.deltaTime * amount;
            }
        }
        resourceBar.fillAmount = activeResource.Value / 100;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceBarUI : ValueReferenceUI
{
    public Image resourceBar;

    void Update()
    {
        resourceBar.fillAmount = activeResource.Value / 100;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSlotUI : ValueReferenceUI
{
    public TextMeshProUGUI text;

    public void Update()
    {
        text.text = "x" + activeResource.Value.ToString();
    }
}

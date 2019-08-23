using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayBar : MonoBehaviour
{
    public FloatVariable value;
    [Space]
    public Canvas canvas;
    public Image colorBar;

    private bool hidden;

    void Update()
    {
        if(value.Value != 0 && !hidden)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
        colorBar.fillAmount = value.Value / value.ConstantValue;
    }

    public void Hide()
    {
        hidden = true;
    }

    public void Show()
    {
        hidden = false;
    }
}

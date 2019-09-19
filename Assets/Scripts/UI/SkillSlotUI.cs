using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    public Image coolDownImage;

    public FloatVariable cooldownTimer;

    public void StartCooldown()
    {
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        while(cooldownTimer.Value > 0)
        {
            coolDownImage.fillAmount = (cooldownTimer.ConstantValue - cooldownTimer.Value) / cooldownTimer.ConstantValue;
            yield return null;
        }

        coolDownImage.fillAmount = cooldownTimer.Value;
    }


}

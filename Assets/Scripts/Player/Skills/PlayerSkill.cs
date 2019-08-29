using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("Skill Properties:")]
    public GameManager gameManager;
    public Character classSkill;

    [Space]
    public bool useResource;
    public float resourceCost;
    public FloatVariable mainResource;
    [Space]
    public bool useCooldown;
    public float cooldownTime;
    public FloatVariable cooldown;
    private bool onCooldown;
    [Space]
    public bool isInstant;
    public bool waitingConfirmation;

    public void TryToCast()
    {
        if (gameManager.characterClass != classSkill)
            return;


        if (isInstant)
        {
            CastSkill();
        }
        else
        {
            waitingConfirmation = !waitingConfirmation;
        }
        //if (useResource)
        //{
        //    if(mainResource.Value >= resourceCost)
        //    {
        //        if(useCooldown && !onCooldown)
        //        {
        //            if (isInstant)
        //            {
        //                CastSkill();
        //            }
        //            else
        //            {
        //                waitingConfirmation = true;
        //            }
        //        }
        //    }
        //}
    }

    public virtual void CastSkill() { }
    public virtual void WaitingConfirmation() { }
}

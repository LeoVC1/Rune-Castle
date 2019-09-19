using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("Manager:")]
    public InputManager inputManager;
    public GameManager gameManager;

    [Header("Skill Properties:")]
    public Character classSkill;

    [Space]
    public bool useResource;
    public float resourceCost;
    public FloatVariable mainResource;
    [Space]
    public bool useCooldown;
    public FloatVariable cooldown;
    public GameEvent onCooldownStart;
    private bool onCooldown;
    [Space]
    public bool isInstant;
    public bool waitingConfirmation;

    public PlayerAnimation anim;

    public void Start()
    {
        anim = GetComponent<PlayerAnimation>();
    }

    public void TryToCast()
    {
        if (gameManager.characterClass != classSkill)
            return;

        //if (useCooldown && !onCooldown)
        //{
        //    if (isInstant)
        //    {
        //        CastSkill();
        //    }
        //    else
        //    {
        //        waitingConfirmation = !waitingConfirmation;
        //        inputManager.isCastingSpell = waitingConfirmation;
        //    }
        //}

        if (useResource)
        {
            if (mainResource.Value >= resourceCost)
            {
                if (useCooldown && !onCooldown)
                {
                    if (isInstant)
                    {
                        CastSkill();
                    }
                    else
                    {
                        waitingConfirmation = !waitingConfirmation;
                        inputManager.isCastingSpell = waitingConfirmation;
                    }
                }
            }
        }
    }

   public IEnumerator Cooldown()
    {
        onCooldown = true;
        cooldown.Value = 0;
        while(cooldown.Value < cooldown.ConstantValue)
        {
            cooldown.Value += Time.deltaTime;
            yield return null;
        }
        cooldown.Value = 0;
        onCooldown = false;
    }

    public virtual void CastSkill() { }
    public virtual void WaitingConfirmation() { }
}

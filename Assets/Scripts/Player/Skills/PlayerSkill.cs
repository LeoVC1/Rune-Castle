using System.Collections;
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

    public PlayerSkill[] otherSkills;
    public void Start()
    {
        anim = GetComponent<PlayerAnimation>();
        otherSkills = GetComponents<PlayerSkill>();
    }

    public void TryToCast()
    {
        if (gameManager.characterClass != classSkill)
            return;

        CancelOtherSkills();

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
        else
        {
            waitingConfirmation = !waitingConfirmation;
            inputManager.isCastingSpell = waitingConfirmation;
        }
        EnableOtherSkills(!waitingConfirmation);
    }

    public void CancelOtherSkills()
    {
        foreach(PlayerSkill skill in otherSkills)
        {
            if (skill.waitingConfirmation && skill != this)
            {
                skill.TryToCast();
            }
        }
    }

    public void EnableOtherSkills(bool value)
    {
        foreach(PlayerSkill skill in otherSkills)
        {
            if(skill != this)
                skill.enabled = value;
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

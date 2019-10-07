using System.Collections;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("Manager:")]
    public InputManager inputManager;
    public GameManager gameManager;
    public InventoryManager inventoryManager;

    [Header("Skill Properties:")]
    public Character classSkill;

    [Space]
    public bool useResource;
    public float resourceCost;
    public FloatVariable mainResource;
    [Space]
    public bool useItem;
    public Item itemResource;
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

    public virtual void Start()
    {
        anim = GetComponent<PlayerAnimation>();
        otherSkills = GetComponents<PlayerSkill>();
    }

    public void TryToCast()
    {
        if (gameManager.characterClass != classSkill)
            return;

        CancelOtherSkills();

        if (useResource || useItem)
        {
            if (mainResource.Value >= resourceCost || inventoryManager.CheckItemAcquirement(itemResource) > 0)
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
                    }
                }
            }
        }
        else
        {
            waitingConfirmation = !waitingConfirmation;
        }

        if (waitingConfirmation)
            inputManager.isWaitingConfirmEvent = true;
        else
            inputManager.isWaitingConfirmEvent = false;

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
            {
                skill.DestroyPreview();
                skill.enabled = value;
            }
        }
    }

    public virtual void DestroyPreview() { }

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

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (!inputManager)
        {
            inputManager = Resources.Load("Managers/InputManager") as InputManager;
        }
        if (!gameManager)
        {
            gameManager = Resources.Load("Managers/GameManager") as GameManager;
        }
        if (!inventoryManager)
        {
            inventoryManager = Resources.Load("Managers/InventoryManager") as InventoryManager;
        }
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}

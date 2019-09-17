using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using RootMotion.Demos;

public class MageBasicAttack : MonoBehaviour
{
    public InputManager inputManager;

    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    public AimIK playerIK;
    public MageAttackPoint point;

    public FBIKBoxing hand;

    public float attackDelay;
    private float delayTimer;
    private bool onDelay;

    [SerializeField] private float weight;
    [SerializeField] private float hitWeight;

    public Transform spine;


    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        //playerIK = GetComponent<AimIK>();
    }

    private void Update()
    {
        SetWeight();

        if (onDelay)
            return;

        //if (playerMovement.isRunning)
        //    return;

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void SetWeight()
    {
        playerIK.solver.IKPositionWeight = weight;
        hand.hitWeight = hitWeight;
    }

    void Attack()
    {
        if (inputManager.isCastingSpell)
            return;

        onDelay = true;

        StartCoroutine(Delay());

        float rnd = Random.Range(0, 2);

        playerMovement.FreezeMovement();

        hand.effector = rnd == 0 ? FullBodyBipedEffector.RightHand : FullBodyBipedEffector.LeftHand;

        point.SetNewOffset(rnd == 0 ? -0.5f : 0.5f);

        playerAnimation.SetTrigger(rnd == 0 ? "_BasicAttack_1" : "_BasicAttack_2");
    }

    IEnumerator Delay()
    {
        delayTimer = 0;
        while(delayTimer < attackDelay)
        {
            delayTimer += Time.deltaTime;
            yield return null;
        }
        onDelay = false;
    }
}

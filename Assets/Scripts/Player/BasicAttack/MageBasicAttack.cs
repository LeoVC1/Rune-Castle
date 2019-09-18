using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using RootMotion.Demos;

public class MageBasicAttack : MonoBehaviour
{
    public GameManager gameManager;
    public InputManager inputManager;

    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private AimIK playerIK;
    private FBIKBoxing handIK;
    public MageAttackPoint point;
    public CameraCollision pointCollision;
    

    public float attackDelay;
    private float delayTimer;
    private bool onDelay;

    [SerializeField] private float weight;
    [SerializeField] private float hitWeight;

    public Transform spine;

    private Animator anim;

    public ParticleSystem pSystem;


    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        playerIK = GetComponent<AimIK>();
        handIK = GetComponent<FBIKBoxing>();
        OnChangeClass();
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

    public void EmitParticle()
    {
        pSystem.Emit(1);
    }

    void SetWeight()
    {
        playerIK.solver.IKPositionWeight = weight;
        handIK.hitWeight = hitWeight;
    }

    void Attack()
    {
        if (inputManager.isCastingSpell)
            return;

        onDelay = true;

        StartCoroutine(Delay());

        float rnd = Random.Range(0, 2);

        float horizontalSpeed = playerAnimation.GetFloat("_Horizontal");

        rnd = horizontalSpeed == 0 ? rnd : horizontalSpeed > 0 ? 1 : 0;

        playerMovement.FreezeMovement();

        handIK.effector = rnd == 0 ? FullBodyBipedEffector.RightHand : FullBodyBipedEffector.LeftHand;

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

    public void OnChangeClass()
    {
        bool isMage = gameManager.characterClass == Character.MAGE ? true : false;
        this.enabled = isMage;
        playerIK.enabled = isMage;
        point.enabled = isMage;
        pointCollision.enabled = isMage;
        handIK.enabled = isMage;
    }
}

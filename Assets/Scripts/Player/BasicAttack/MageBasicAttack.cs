using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using RootMotion.Demos;

public class MageBasicAttack : MonoBehaviour
{
    [Header("Managers:")]
    public GameManager gameManager;
    public InputManager inputManager;

    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private AimIK playerIK;
    private FBIKBoxing handIK;
    private Animator anim;
    private SpawnProjectilesScript spawnProjectiles;

    [Header("References:")]
    public MageAttackPoint point;
    public CameraCollision pointCollision;
    public Transform pointTransform;
    public Transform head;
    public GameObject VFX;

    [Header("Properties:")]
    public LayerMask layerMask;
    public float attackDelay;
    private float delayTimer;
    private bool onDelay;

    [SerializeField] private float weight;
    [SerializeField] private float hitWeight;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        playerIK = GetComponent<AimIK>();
        handIK = GetComponent<FBIKBoxing>();
        spawnProjectiles = GetComponent<SpawnProjectilesScript>();
        OnChangeClass();
    }

    private void Update()
    {
        if (!inputManager.canAttack)
            return;

        if (inputManager.isWaitingConfirmEvent)
            return;

        if (inputManager.isCastingSpell)
            return;

        SetWeight();

        if (onDelay)
            return;

        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    private void LateUpdate()
    {
        Vector3 direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        direction.y = transform.forward.y;
        head.forward = (Vector3.Slerp(head.forward, direction, 1f));
    }

    public void EmitParticle()
    {
        Vector3 hitPoint = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out RaycastHit hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            hitPoint = hit.point;
        }
        else
        {
            hitPoint = Camera.main.transform.position + Camera.main.transform.forward * 100;
        }

        GameObject vfx = Instantiate(VFX, pointTransform.position, Quaternion.identity);

        vfx.transform.forward = hitPoint - vfx.transform.position;
    }

    void SetWeight()
    {
        playerIK.solver.IKPositionWeight = weight;
        handIK.hitWeight = hitWeight;
    }

    void Attack()
    {
        onDelay = true;

        float rnd = Random.Range(0, 2);

        float horizontalSpeed = playerAnimation.GetFloat("_Horizontal");

        bool isMoving = horizontalSpeed == 0 ? false : true;

        rnd = horizontalSpeed == 0 ? rnd : horizontalSpeed > 0 ? 1 : 0;

        playerMovement.FreezeMovement();

        handIK.effector = rnd == 0 ? FullBodyBipedEffector.RightHand : FullBodyBipedEffector.LeftHand;

        ResetIK();

        point.SetNewOffset(isMoving == false ? (rnd == 0 ? 0.7f : -0.7f) : (rnd == 0 ? 0.3f : -0.3f));

        pointCollision.SetNewDistance(isMoving == false ? 2.8f : 3.2f);

        playerAnimation.SetTrigger(rnd == 0 ? "_BasicAttack_1" : "_BasicAttack_2");
    }
    public void ResetIK()
    {
        playerIK.solver.IKPositionWeight = 0;
        handIK.hitWeight = 0;
    }

    public void FinishDelay()
    {
        onDelay = false;
    }

    public void OnChangeClass()
    {
        bool isMage = gameManager.characterClass == Character.MAGE ? true : false;
        this.enabled = isMage;
        playerIK.enabled = isMage;
        handIK.enabled = isMage;
    }

    public void LockBasicAttack()
    { 
        point.enabled = false;
        pointCollision.enabled = false;
    }

    public void UnlockBasicAttack()
    {
        playerIK.enabled = true;
        point.enabled = true;
        pointCollision.enabled = true;
        handIK.enabled = true;
    }
}

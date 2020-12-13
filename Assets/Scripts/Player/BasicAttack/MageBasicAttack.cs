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

    [Header("References:")]
    public MageAttackPoint point;
    public Transform pointTransform;
    public Transform head;
    public GameObject VFX;
    public GameObject rightHandOrb;
    public GameObject leftHandOrb;

    [Header("Properties:")]
    public LayerMask layerMask;
    private bool onDelay;

    public AudioSource shot;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
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
        rightHandOrb.SetActive(false);
        leftHandOrb.SetActive(false);

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

    void Attack()
    {
        onDelay = true;

        float rnd = Random.Range(0, 2);

        float horizontalSpeed = playerAnimation.GetFloat("_Horizontal");

        bool isMoving = horizontalSpeed == 0 ? false : true;

        rnd = horizontalSpeed == 0 ? rnd : horizontalSpeed > 0 ? 1 : 0;

        if (rnd == 0)
            rightHandOrb.SetActive(true);
        else
            leftHandOrb.SetActive(true);

        playerMovement.FreezeMovement();

        point.SetNewOffset(isMoving == false ? (rnd == 0 ? 1.2f : -1.2f) : (rnd == 0 ? 0.5f : -0.5f));

        playerAnimation.SetTrigger(rnd == 0 ? "_BasicAttack_1" : "_BasicAttack_2");

        shot.Play();
    }

    public void FinishDelay()
    {
        onDelay = false;
    }

    public void OnChangeClass()
    {
        bool isMage = gameManager.characterClass == Character.MAGE ? true : false;
        this.enabled = isMage;
    }

    public void LockBasicAttack()
    { 
        point.enabled = false;
    }

    public void UnlockBasicAttack()
    {
        point.enabled = true;
    }
}

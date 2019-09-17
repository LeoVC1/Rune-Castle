using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBasicAttack : MonoBehaviour
{
    public InputManager inputManager;

    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;

    public float attackDelay;
    private float delayTimer;
    private bool onDelay;

    public Transform spine;

    private bool attack;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (onDelay)
            return;

        if (playerMovement.isRunning)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void LateUpdate()
    {
        //if (!onDelay)
        //    return;

        //if (attack)
        //{
        //    Vector3 direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        //    direction.y = transform.forward.y;
        //    spine.forward = (Vector3.Slerp(spine.forward, direction, 0.8f));

        //    Attack();
        //    attack = false;
        //}
    }

    void Attack()
    {
        if (inputManager.isCastingSpell)
            return;

        float rnd = Random.Range(0, 2);

        playerMovement.FreezeMovement();

        playerAnimation.SetTrigger(rnd == 0 ? "_BasicAttack_1" : "_BasicAttack_2");
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        onDelay = true;
        delayTimer = 0;
        while(delayTimer < attackDelay)
        {
            delayTimer += Time.deltaTime;
            yield return null;
        }
        onDelay = false;
    }
}

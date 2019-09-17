using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBasicAttack : MonoBehaviour
{
    public InputManager inputManager;

    private PlayerAnimation playerAnimation;

    public float attackDelay;
    private float delayTimer;
    private bool onDelay;

    public Transform spine;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void FixedUpdate()
    {
        if (onDelay)
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


        //Vector3 direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        //direction.y = transform.forward.y;
        //Vector3 newForward = (Vector3.Slerp(spine.forward, direction, 0.8f));

        //spine.forward = newForward;
    }

    void Attack()
    {
        if (inputManager.isCastingSpell)
            return;

        playerAnimation.SetTrigger("_BasicAttack_1");
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

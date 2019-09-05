using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageArea : PlayerSkill
{
    [Header("Manager:")]
    public InputManager inputManager;

    [Header("Attack Objects:")]
    public GameObject attackParticle;
    public GameObject attackPreview;

    public GameEventListener listener;

    private GameObject attackPreviewInstance;
    private Vector3 attackPosition;

    [Range(1, 30)]
    public float attackRange;
    [Range(1, 30)]
    public float attackRadius;

    public float moveSkillSpeed = 5;

    private void Update()
    {
        WaitingConfirmation();
    }

    public override void CastSkill()
    {
        DestroyPreview();
        GameObject attack = Instantiate(attackParticle, attackPosition, Quaternion.identity);
    }

    public override void WaitingConfirmation()
    {
        if (waitingConfirmation)
        {
            listener.enabled = true;
            inputManager.LockMovement();
            if (attackPreviewInstance == null)
            {
                attackPreviewInstance = Instantiate(attackPreview);
                attackPreviewInstance.transform.position = (transform.position - (new Vector3(0, 1.4f, 0))) + transform.forward * attackRange;
            }
            else
            {
                attackPreviewInstance.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * moveSkillSpeed, 0, Input.GetAxis("Vertical") * Time.deltaTime * moveSkillSpeed);
            }
            attackPosition = attackPreviewInstance.transform.position;
        }
        else
        {
            inputManager.UnlockMovement();
            listener.enabled = false;
            if (attackPreviewInstance != null)
            {
                Destroy(attackPreviewInstance);
                attackPreviewInstance = null;
            }
        }
    }

    public void DestroyPreview()
    {
        Destroy(attackPreviewInstance);
        attackPreviewInstance = null;
        waitingConfirmation = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (waitingConfirmation)
        {
            float theta = 0;
            float x = attackRadius * Mathf.Cos(theta);
            float y = attackRadius * Mathf.Sin(theta);
            Vector3 pos = attackPosition + new Vector3(x, 0, y);
            Vector3 newPos = pos;
            Vector3 lastPos = pos;
            for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
            {
                x = attackRadius * Mathf.Cos(theta);
                y = attackRadius * Mathf.Sin(theta);
                newPos = attackPosition + new Vector3(x, 0, y);
                Gizmos.DrawLine(pos, newPos);
                pos = newPos;
            }
            Gizmos.DrawLine(pos, lastPos);
        }
    }

}

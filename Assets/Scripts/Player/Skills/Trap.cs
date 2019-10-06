using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : PlayerSkill
{
    [Header("Totem Properties:")]
    public GameObject trapPrefab;
    public GameObject trapPreview;
    public GameEventListener listener;
    public LayerMask layerMask;

    [Range(1, 30)]
    public float attackRange;

    private GameObject trapPreviewInstance;
    private Vector3 trapLocation;

    [Header("Animation Properties:")]
    public string animationParameter;

    private void Update()
    {
        WaitingConfirmation();
    }

    public override void CastSkill()
    {
        if (this.enabled)
        {
            inputManager.isCastingSpell = true;
            inputManager.isWaitingConfirmEvent = false;

            inventoryManager.RemoveItem(itemResource);

            DestroyPreview();

            Animate();

            inputManager.FreezeCamera();
            inputManager.LockMovement();

            GameObject totem = Instantiate(trapPrefab);
            totem.transform.position = trapLocation;
        }
    }

    public override void WaitingConfirmation()
    {
        if (waitingConfirmation)
        {
            listener.enabled = true;
            Vector3 mousePosition = GetMousePosition();
            if (trapPreviewInstance == null)
            {
                trapPreviewInstance = Instantiate(trapPreview);
                trapPreviewInstance.transform.position = mousePosition + Vector3.up * 1.6f;
            }
            else
            {
                trapPreviewInstance.transform.position = mousePosition + Vector3.up * 1.6f;
            }
            trapLocation = trapPreviewInstance.transform.position;
        }
        else
        {
            listener.enabled = false;
            if (trapPreviewInstance != null)
            {
                Destroy(trapPreviewInstance);
                trapPreviewInstance = null;
            }
        }
    }

    public void DestroyPreview()
    {
        Destroy(trapPreviewInstance);
        trapPreviewInstance = null;
        waitingConfirmation = false;
    }

    private void Animate()
    {
        anim.SetTrigger(animationParameter);
    }

    public void OnAnimationEnd()
    {
        inputManager.UnfreezeCamera();
        inputManager.UnlockMovement();
        inputManager.isCastingSpell = false;
        inputManager.canAttack = true;
        EnableOtherSkills(true);
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider)
            {
                mousePosition = hit.point;
            }
        }
        return mousePosition;
    }

}

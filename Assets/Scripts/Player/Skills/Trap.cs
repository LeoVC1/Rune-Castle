using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : PlayerSkill
{
    [Header("Trap Properties:")]
    public GameObject trapPrefab;
    public GameObject trapPreview;
    public GameEventListener listener;
    public LayerMask layerMask;

    [Range(1, 30)]
    public float attackRange;

    private MageBasicAttack basicAttack;
    private GameObject trapPreviewInstance;
    private Vector3 trapLocation;

    public override void Start()
    {
        base.Start();
        basicAttack = GetComponent<MageBasicAttack>();
    }

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
            inputManager.FreezeCamera();

            inventoryManager.RemoveItem(itemResource);

            DestroyPreview();

            mainResource.Value -= resourceCost;

            GameObject totem = Instantiate(trapPrefab);
            totem.transform.position = trapLocation;

            EnableOtherSkills(true);

            Invoke("UnlockBasicAttack", 0.2f);
        }
    }

    void UnlockBasicAttack()
    {
        basicAttack.UnlockBasicAttack();
        inputManager.UnfreezeCamera();
        inputManager.isCastingSpell = false;
        inputManager.canAttack = true;
        EnableOtherSkills(true);
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
                trapPreviewInstance.transform.position = mousePosition + Vector3.up * 0.2f;
            }
            else
            {
                trapPreviewInstance.transform.position = mousePosition + Vector3.up * 0.2f;
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

    public override void DestroyPreview()
    {
        Destroy(trapPreviewInstance);
        trapPreviewInstance = null;
        waitingConfirmation = false;
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

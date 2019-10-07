using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : PlayerSkill
{
    [Header("Totem Properties:")]
    public GridManager gridManager;
    public GameObject totemPrefab;
    public GameObject totemPreview;
    public GameEventListener listener;
    public LayerMask layerMask;

    [Range(1, 30)]
    public float attackRange;

    private GameObject totemPreviewInstance;
    private Transform totemRangeInstance;
    private Vector3 totemLocation;

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
            
            DestroyPreview();

            Animate();

            inputManager.FreezeCamera();
            inputManager.LockMovement();

            GameObject totem = Instantiate(totemPrefab);
            totem.transform.position = totemLocation;
        }
    }

    public override void WaitingConfirmation()
    {
        //if (waitingConfirmation)
        //{
        //    listener.enabled = true;
        //    if (totemPreviewInstance == null)
        //    {
        //        totemPreviewInstance = Instantiate(totemPreview);
        //        totemPreviewInstance.transform.position = GetMousePosition();
        //        totemPreviewInstance.transform.localScale = new Vector3(gridManager.cellSize, gridManager.cellSize, gridManager.cellSize);
        //    }
        //    else
        //    {
        //        (Vector3 newPosition, Vector3 ID) = gridManager.GetClosestPoint(GetMousePosition());
        //        if (!gridManager.UsingID(ID))
        //            totemPreviewInstance.transform.position = newPosition;
        //    }
        //}
        //else
        //{
        //    listener.enabled = false;
        //    if (totemPreviewInstance != null)
        //    {
        //        Destroy(totemPreviewInstance);
        //        totemPreviewInstance = null;
        //    }
        //}
        if (waitingConfirmation)
        {
            listener.enabled = true;
            Vector3 mousePosition = GetMousePosition();
            if (totemPreviewInstance == null)
            {
                totemPreviewInstance = Instantiate(totemPreview);
                totemPreviewInstance.transform.position = mousePosition + Vector3.up * 1.6f;
            }
            else
            {
                totemPreviewInstance.transform.position = mousePosition + Vector3.up * 1.6f;
            }
            totemLocation = totemPreviewInstance.transform.position;
        }
        else
        {
            listener.enabled = false;
            if (totemPreviewInstance != null)
            {
                Destroy(totemPreviewInstance);
                totemPreviewInstance = null;
            }
        }
    }

    public override void DestroyPreview()
    {
        Destroy(totemPreviewInstance);
        totemPreviewInstance = null;
        totemRangeInstance = null;
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

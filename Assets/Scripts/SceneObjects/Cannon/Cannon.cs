using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Interactable
{
    public GameObject projectil;
    public LineRenderer lineRenderer;

    public void Shoot()
    {
        Instantiate(projectil, transform.position + new Vector3(0.5f, 0.5f, 0), transform.rotation);
    }

    public override void TriggerEnter()
    {
        base.TriggerEnter();
        lineRenderer.enabled = true;
    }

    public override void TriggerExit()
    {
        base.TriggerExit();
        lineRenderer.enabled = false;
    }
}

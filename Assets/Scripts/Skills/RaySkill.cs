using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySkill : MonoBehaviour
{
    [Header("References:")]
    public LineRenderer myLineRenderer;
    public LineRenderer[] otherLineRenderers;
    public GameObject hitParticle;
    public GameObject enemyHitParticle;
    public GameObject lineHit;


    [Header("Properties:")]
    public float damageOverTime;
    public LayerMask layerMask;
    public LayerMask enemyLayerMask;

    private bool onHit;
    private RaycastHit hit;

    private void Update()
    {
        ActivateRay();
    }

    private void ActivateRay()
    {
        myLineRenderer.SetPosition(0, transform.position);
        foreach (LineRenderer other in otherLineRenderers)
        {
            other.SetPosition(0, transform.position);
        }

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayerMask))
        {
            if (hit.collider)
            {
                onHit = true;
                lineHit.SetActive(false);
                GameObject particle = Instantiate(enemyHitParticle, hit.point, Quaternion.identity);
                particle.transform.up = hit.normal;

                hit.collider.GetComponent<Enemy>().ReceiveDamage(35);
            }
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider)
            {
                //lineHit.SetActive(true);
                //lineHit.transform.position = hit.point;
                //lineHit.transform.up = hit.normal;
                //lineHit.transform.position += Vector3.up * 1;
                onHit = true;
                GameObject particle = Instantiate(hitParticle, hit.point, Quaternion.identity);
                particle.transform.up = hit.normal;
            }
        }
        else
        {
            onHit = false;
        }

        transform.parent.forward = hit.point - transform.position;

        myLineRenderer.SetPosition(1, hit.point);
        foreach (LineRenderer other in otherLineRenderers)
        {
            other.SetPosition(1, hit.point);
        }
    }

}

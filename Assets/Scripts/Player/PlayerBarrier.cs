using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBarrier : MonoBehaviour
{
    public MeshRenderer rend;
    public GameObject player;

    private void Start()
    {
        InvokeRepeating("VerifyDistance", 0, 0.1f);
    }

    void VerifyDistance()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 20)
        {
            rend.enabled = true;
        }
        else
        {
            rend.enabled = false;
        }
    }
}

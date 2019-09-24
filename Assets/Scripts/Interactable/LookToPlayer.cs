using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToPlayer : MonoBehaviour
{
    public bool _2D;
    public bool invertForward;

    void Update()
    {
        if(_2D)
            transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));
        else
            transform.LookAt(Camera.main.transform);

        if(invertForward)
            transform.forward = -transform.forward;
    }
}

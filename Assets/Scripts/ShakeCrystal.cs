using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCrystal : MonoBehaviour
{
    public float speed = 1;
    public float amount = 1;

    Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void LateUpdate()
    {
        ShakeCristal();
    }

    public void ShakeCristal()
    {
        Vector3 pos = transform.position;
        pos.x = Random.Range((Mathf.Sin(speed) * amount), (Mathf.Cos(Time.time * speed) * amount)) + startPosition.x;
        pos.z = Random.Range((Mathf.Sin(speed) * amount), (Mathf.Cos(Time.time * speed) * amount)) + startPosition.z;
        transform.position = pos;
    }

}

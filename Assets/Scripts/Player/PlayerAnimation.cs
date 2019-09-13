using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetMovementSpeed(float speed)
    {
        anim.SetFloat("_Speed", speed);
    }

    public void SetTrigger(string parameter)
    {
        anim.SetTrigger(parameter);
    }
}

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

    public void SetMovementSpeed(float horizontal, float vertical)
    {
        anim.SetFloat("_Vertical", vertical);
        anim.SetFloat("_Horizontal", horizontal);
    }

    public void SetTrigger(string parameter)
    {
        anim.SetTrigger(parameter);
    }
}

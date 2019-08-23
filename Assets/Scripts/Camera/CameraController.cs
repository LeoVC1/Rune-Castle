using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Cam;

public class CameraController : MonoBehaviour
{
    public GameManager gameManager;
    public Transform mage, engineer;

    RTS_Camera RTS_Component;

    private void Start()
    {
        RTS_Component = GetComponent<RTS_Camera>();
        ChangeTarget();
    }

    public void ChangeTarget()
    {
        RTS_Component.targetFollow = gameManager.characterClass == Character.MAGE ? mage : engineer;
    }
}

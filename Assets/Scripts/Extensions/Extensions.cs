using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtensions
{
    public static Ray GetCenterRay(this Camera camera)
    {
        return camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
    }

    public static Vector3 GetCenterDirection(this Camera camera)
    {
        return camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)).direction;
    }
}

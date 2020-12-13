using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueObjects : MonoBehaviour
{
    public GameObject[] gameObjects;
    private int control = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            gameObjects[control].SetActive(false);

            if (control + 1 == gameObjects.Length)
                control = -1;

            control++;
            gameObjects[control].SetActive(true);
        }
    }
}

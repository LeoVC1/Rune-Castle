using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public Image fade;
    public GameObject pause;

    public void Continue()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
    }

    public void Restart()
    {
        StartCoroutine(FadeRestart());
    }

    public void Quit()
    {
        StartCoroutine(FadeHome());
    }

    IEnumerator FadeRestart()
    {
        pause.SetActive(false);
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime;
            Color aux = fade.color;
            aux.a = Mathf.Lerp(0, 1, t);
            yield return null;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FadeHome()
    {
        pause.SetActive(false);
        float t = 0;
        while (t <= 1)
        {
            t += Time.deltaTime;
            Color aux = fade.color;
            aux.a = Mathf.Lerp(0, 1, t);
            fade.color = aux;
            yield return null;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}

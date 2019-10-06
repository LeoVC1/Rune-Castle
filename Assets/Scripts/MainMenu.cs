using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator fadeOut;

    public Animator _camera;
    public Animator _canvas;
    public Animator _character;

    public GameObject[] menuParticle;
    public GameObject selectClassParticle;

    //public LabelMovement mainMenu;
    //public LabelMovement characterSelection;

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartFade()
    {
        fadeOut.SetBool("FadeOut", true);
    }

    public void GoToCharacter()
    {
        StartCoroutine(CameraToCharacter());
    }

    IEnumerator CameraToCharacter()
    {
        _camera.SetInteger("Stage", 1);
        _canvas.SetInteger("Stage", 1);
        _character.SetInteger("Stage", 1);
        foreach (GameObject g in menuParticle)
        {
            g.SetActive(false);
        }
        yield return new WaitForSeconds(0.7f);
        selectClassParticle.SetActive(true);
    }

    public void ReturnToMenu()
    {
        StartCoroutine(CameraToMenu());
    }

    IEnumerator CameraToMenu()
    {
        _camera.SetInteger("Stage", 0);
        _canvas.SetInteger("Stage", 0);
        _character.SetInteger("Stage", 0);
        selectClassParticle.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        foreach (GameObject g in menuParticle)
        {
            g.SetActive(true);
        }
    }
}

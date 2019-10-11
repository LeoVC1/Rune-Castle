using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextWave : MonoBehaviour
{
    int number = 3;
    int wave = 1;

    TextMeshProUGUI textMesh;

    Animator anim;

    public GameEvent startWaveEvent;

    public TextMeshProUGUI waveNumber;

    public AudioSource enemies;
    public AudioSource boss;
    public AudioSource berrante;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
        waveNumber.text = "Wave:" + wave.ToString();
    }

    public void StartAnim()
    {
        number = 3;
        textMesh.text = number.ToString(); ;
        anim.SetTrigger("Wave");
    }

    public void Count()
    {
        number--;
        textMesh.text = number.ToString();
    }

    public void StartWave()
    {
        waveNumber.text = "Wave: " + wave.ToString();
        wave++;
        textMesh.text = "Defend!";
        startWaveEvent.Raise();
        berrante.Play();
        //if(Random.Range(0, 10) > 6)
        //{
        //    enemies.Stop();
        //}
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Eventos : MonoBehaviour
{
	public TextMeshProUGUI nomeJogador;
	public SpawnerManager spawner;

    public void clickGetJogador()
    {
        StartCoroutine(getJogador());
    }
    public void clickGetJogadores()
    {
        StartCoroutine(getJogadores());
    }
    public void clickPostJogador()
    {
        StartCoroutine(postJogador());
    }
    IEnumerator getJogador()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:30947/Api/Jogador/1");
        yield return www.SendWebRequest();
        if(!www.isHttpError && !www.isNetworkError)
        {//Deu certo
            Debug.Log(www.downloadHandler.text);
            Jogador j = JsonUtility.FromJson<Jogador>(www.downloadHandler.text);
        }
        else
        {//Deu Erro
            Debug.Log(www.error);
        }
    }
    IEnumerator postJogador()
    {
        WWWForm form = new WWWForm();
	form.AddField("Id", "0");
	form.AddField("Nome", nomeJogador.text);
	form.AddField("Pontuacao", spawner.waveNumber);
                                         
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:30947/Api/Jogador/", form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www);
        }
    }
    IEnumerator getJogadores()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:30947/Api/Jogador/");
        yield return www.SendWebRequest();
        if (!www.isHttpError && !www.isNetworkError)
        {//Deu certo
            Debug.Log(www.downloadHandler.text);
            Jogador[] j = JsonHelper.FromJson<Jogador>(www.downloadHandler.text);
            foreach(var item in j)
            {
                Debug.Log(item);
            }
        }
        else
        {//Deu Erro
            Debug.Log(www.error);
        }
    }
}

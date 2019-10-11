using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Audio {
    public string name;
    public AudioClip clip;
    [Range(0f,1f)] public float volume;
}

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;

	public AudioSource[] musicSource;
	public AudioSource[] soundSource;

    [Range(0f, 1f)] public float soundVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;

    public Audio[] musicClips;
	public Audio[] soundClips;

	void Awake () {
		DontDestroyOnLoad (gameObject);
		if (instance == null) instance = this;
		else if (instance != this) Destroy (gameObject);
	}

	void Start () {
		PlayMusic (0);
	}

	public void PlayMusic (int indexMusic) {
        //musicSource[1].clip = null;
		musicSource[0].clip = musicClips [indexMusic].clip;
        musicSource[0].volume = musicClips [indexMusic].volume / musicVolume;
        musicSource[0].Play ();
	}

    public void TransitionMusic (int indexMusic, float speed) {
        //StopCoroutine(Transition(indexMusic));
        StartCoroutine(Transition(indexMusic, speed));
    }

    /*public IEnumerator Transition (int indexMusic) {
        float speed = 0.15f;
        float volume = musicClips [indexMusic].volume / musicVolume;

        while (musicSource.volume > 0) {
            musicSource.volume -= speed;
            yield return null;
        }

        musicSource.clip = musicClips [indexMusic].clip;
        musicSource.Play();

        while (musicSource.volume < volume) {
            musicSource.volume += speed;
            yield return null;
        }

        musicSource.volume = volume;
    }*/

    public IEnumerator Transition (int indexMusic, float speed) {
        //float speed = 0.02f;
        int primary = musicSource[0].clip != null ? 0 : 1;
        int secondary = musicSource[0].clip != null ? 1 : 0;
        float volumeSecondary = musicClips[secondary].volume / musicVolume;

        musicSource[secondary].clip = musicClips[indexMusic].clip;
        musicSource[secondary].volume = 0;
        musicSource[secondary].Play ();

        while (musicSource[primary].volume > 0 && musicSource[secondary].volume < volumeSecondary) {
            musicSource[primary].volume -= speed;
            musicSource[secondary].volume += speed;
            yield return null;
        }

        musicSource[primary].clip = null;
    }

    public void PlaySound (string nameSound, Vector3 pos) {
		for (int i = 0; i < soundSource.Length; i++) {
			if (!soundSource[i].isPlaying) {
                int indexSound = ReturnIndexSound(nameSound);
                soundSource[i].transform.position = pos;
                soundSource [i].clip = soundClips [indexSound].clip;
                soundSource[i].volume = soundClips[indexSound].volume / soundVolume;
				soundSource [i].Play ();
				i = soundSource.Length;
			}
		}
	}

	//public void AttVolume () { /// AJUSTAR PQ TA ERRADO
	//	/*musicSource.volume = (float) PlayerPrefs.GetInt ("VolumeMusic") / 100f;
	//	for (int i = 0; i < soundSource.Length; i++) {
	//		soundSource [i].volume = (float) PlayerPrefs.GetInt ("VolumeSound") / 100f;
	//	}*/

 //       // Variaveis Provisorias pelo bug do Save
 //       musicSource[0].volume = Menu.instance.musicSlider.value;
 //       musicSource[1].volume = Menu.instance.musicSlider.value;
 //       for (int i = 0; i < soundSource.Length; i++) {
 //           soundSource[i].volume = Menu.instance.soundSlider.value;
 //       }
 //   }

    int ReturnIndexSound (string name) {
        for (int i = 0; i < soundClips.Length; i++) {
            if (soundClips[i].name == name) {
                return i;
            }
        }
        return 0;
    }
}
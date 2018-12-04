using System;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{

    public AudioSource efxSource;
    public AudioSource musicSource;

    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    public Sprite soundOnImage;
    public Sprite soundOffImage;

    private GameObject sound;
    private int soundOn;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            soundOn = PlayerPrefs.GetInt("soundOn", 1);
            if(soundOn == 1)
            {
                musicSource.Play();
            }
        }
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void soundInit(GameObject SoundInit)
    {
        sound = SoundInit;
        refreshIcon();
    }

    public void refreshIcon()
    {
        if (soundOn == 1)
            sound.GetComponent<Image>().sprite = soundOnImage;
        else
            sound.GetComponent<Image>().sprite = soundOffImage;
    }

    public void soundClick()
    {
        if (soundOn == 0)
        {
            musicSource.Play();
            soundOn = 1;
        }
        else
        { 
            musicSource.Pause();
            soundOn = 0;
        }
        PlayerPrefs.SetInt("soundOn", soundOn);

        refreshIcon();
    }

    public void PlaySingle(AudioClip clip)
    {
        if (soundOn == 1)
        {
            efxSource.clip = clip;
            efxSource.Play();
        }
    }
}

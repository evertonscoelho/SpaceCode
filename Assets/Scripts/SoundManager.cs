using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour {

    public AudioSource efxSource;                  
    public AudioSource musicSource;      
    
    public static SoundManager instance = null;
    
    public float lowPitchRange = .95f;              
    public float highPitchRange = 1.05f;

    public Sprite soundOnImage;
    public Sprite soundOffImage;

    private GameObject sound;
    private Boolean soundOn;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            soundOn = true;
            musicSource.Play();
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
        if (soundOn)
            sound.GetComponent<Image>().sprite = soundOnImage;
        else
            sound.GetComponent<Image>().sprite = soundOffImage;
    }

    public void soundClick()
    {
        soundOn = !soundOn;
        if (soundOn)
            musicSource.Play();
        else
            musicSource.Pause();

        refreshIcon();

    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }


}

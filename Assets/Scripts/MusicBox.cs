using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* A class to play background music through the level
* set up so the music changes between day and night
*/
public class MusicBox : MonoBehaviour
{
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] AudioClip dayMusic;
    [SerializeField] AudioClip nightMusic;

    [SerializeField] bool dayMode; //is the music currently in day mode?
    [SerializeField] LightCycle lightCycle;

    [SerializeField] float volMax; //Max volume for the music

    [SerializeField,Range(0,24)] float time;

    float frameCounter;

    // Start is called before the first frame update
    void Start()
    {
        lightCycle = GameObject.Find("Sunlight").GetComponent<LightCycle>();
        time = lightCycle.timeOfDay;
        musicPlayer.clip = dayMusic;
        musicPlayer.volume = volMax;
        dayMode = true;
        musicPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        time = lightCycle.timeOfDay;
        fade();

        if(time > 0 && time < 12 && !dayMode)
        {
            musicPlayer.clip = dayMusic;
            musicPlayer.Stop();
            musicPlayer.Play();
            dayMode = true;
        }
        else if(time > 12 && dayMode)
        {
            musicPlayer.clip = nightMusic;
            musicPlayer.Stop();
            musicPlayer.Play();
            dayMode = false;
        }
        else
        {
            //continue playing
            musicPlayer.volume = volMax;
        }
    }

    /**
    *   Fades the music in and out at specific day times
    */
    void fade()
    {
        float fader;
        if(time > 11.5f && time < 12f)
        {
            fader = (0.5f - (12f - time))/0.5f;
            musicPlayer.volume = Mathf.Lerp(volMax,0,fader);
        }
        else if(time > 23.5f && time < 24f)
        {
            fader = (0.5f - (24f - time))/0.5f;
            musicPlayer.volume = Mathf.Lerp(volMax,0,fader);
        }
        else if(time > 0 && time < 0.5)
        {
            fader = (0.5f - (0.5f - time))/0.5f;
            musicPlayer.volume = Mathf.Lerp(0,volMax,fader);
        }
        else if(time > 12f && time < 12.5f)
        {
            fader = (0.5f - (12.5f - time))/0.5f;
            musicPlayer.volume = Mathf.Lerp(0,volMax,fader);
        }
        else
        {
            musicPlayer.volume = volMax;
        }
    }

}
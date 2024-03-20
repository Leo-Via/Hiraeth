using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------Audio Source---------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;



    [Header("------------Audio Clips---------------")]
    public AudioClip Background;
    public AudioClip RunningOnGrass;
    public AudioClip MissAttack;
    public AudioClip Attack;
    public AudioClip Jump;

    private void Start() 
    {
        musicSource.clip = Background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    /* public void Offmusic()
     {
         musicSource.Stop();
     }*/

    // Flag to track music state
    private bool isMusicPlaying = true; 

    public void ToggleMusic()
    {
        if (isMusicPlaying)
        {
            musicSource.Pause();
            SFXSource.Pause();
        }
        else
        {
            musicSource.Play();
            SFXSource.Play();
        }


        // Toggle music state
        isMusicPlaying = !isMusicPlaying; 
    }


}


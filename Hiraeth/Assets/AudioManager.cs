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

}

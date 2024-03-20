using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;


    /*private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume(); 
        }
        else 
        {
            SetMusicVolume(); 
        }
    }*/

    public void SetMusicVolume()
    { 
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
       // PlayerPrefs.SetFloat("musicVolume", volume);
    
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        // PlayerPrefs.SetFloat("SFXVolume", volume);

    }


    /*
     private void LoadVolume()
     {
         musicSlider.Value = PlayerPrefs.GetFloat("musicVolume");
         SetMusicVolume();

     }
    */
}

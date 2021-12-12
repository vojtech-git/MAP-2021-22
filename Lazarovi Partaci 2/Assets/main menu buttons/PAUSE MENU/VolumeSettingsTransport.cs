using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettingsTransport : MonoBehaviour
{
    public AudioMixer theMixer; 
    public Text masterLabel, musicLabel, sfxLabel;

    public Slider masterSlider, musicSlider, sfxSlider;

    void Start()
    {
        float vol = 0f;
        if(PlayerPrefs.HasKey("MasterVol")){
        theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));

        theMixer.GetFloat("MasterVol", out vol);
        masterSlider.value = vol; 
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        }

        if(PlayerPrefs.HasKey("MusicVol")){
        theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        theMixer.GetFloat("MusicVol", out vol);
        musicSlider.value = vol;
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        }

        if(PlayerPrefs.HasKey("SfxVol")){
        theMixer.SetFloat("SfxVol", PlayerPrefs.GetFloat("SfxVol"));
        theMixer.GetFloat("SfxVol", out vol);
        sfxSlider.value = vol;
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString(); 
        }



    }
}

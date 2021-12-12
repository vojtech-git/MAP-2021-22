using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer theMixer; 
    public Text masterLabel, musicLabel, sfxLabel;

    public Slider masterSlider, musicSlider, sfxSlider;
    public SettingScreen sc;
    void Start()
    {
        float vol=0f;  //zobrazeni predchozi promenne do slideru

         if(PlayerPrefs.HasKey("savedQualityLevel"))
        {
         int savedIndex;
         savedIndex =  PlayerPrefs.GetInt("savedQualityLevel");
         QualitySettings.SetQualityLevel(savedIndex);
         if(savedIndex == 0) {
             sc.selectedGraphicsLevel = 0;
             sc.UpdateGraphicsLabel();
         }
          if(savedIndex == 1) {
             sc.selectedGraphicsLevel = 1;
             sc.UpdateGraphicsLabel();
         }
          if(savedIndex == 2) {
             sc.selectedGraphicsLevel = 2;
             sc.UpdateGraphicsLabel();
         }

        }

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

    // Update is called once per frame
   
}

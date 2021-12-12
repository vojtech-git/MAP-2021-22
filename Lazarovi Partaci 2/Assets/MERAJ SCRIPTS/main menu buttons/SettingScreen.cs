using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SettingScreen : MonoBehaviour
{
    public Toggle fullScreenToggle;
    public Toggle vsyncToggle;
    public Text resolutionLabel;
    public List <ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;

    public int selectedGraphicsLevel = 2;
    public Text graphichsLevelLabel;

    public AudioMixer theMixer;

    public Text masterLabel, musicLabel, sfxLabel;

    public Slider masterSlider, musicSlider, sfxSlider;


    void Start()
    {
        fullScreenToggle.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            vsyncToggle.isOn = false;
        }
        else 
        {
            vsyncToggle.isOn = true;
        }


        bool foundResolution = false;
        for (int i=0; i< resolutions.Count; i++)
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)  //abychom zjistili co prave mame,    
            {
                foundResolution = true;
                selectedResolution = i;
                UpdateResolutionLabel();
            }
        }


        if(!foundResolution){

            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedResolution  = resolutions.Count-1;
            UpdateResolutionLabel();
        }


       /*  float vol=0f;  //zobrazeni predchozi promenne do slideru
        theMixer.GetFloat("MasterVol", out vol);
        masterSlider.value = vol;

        theMixer.GetFloat("MusicVol", out vol);
        musicSlider.value = vol;

        theMixer.GetFloat("SfxVol", out vol);
        sfxSlider.value = vol;

        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString(); */

        //KVULI FUNKCIONALITE BYL TENTO KUS KODU PRENES DO AUDIOMANAGERA, IDK PROC ZE DNE NA DEN TO PRESTALO FUNGOVATI
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ApplyGraphics() 
    {
        //Screen.fullScreen = fullScreenToggle.isOn;

        if(vsyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else 
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullScreenToggle.isOn);
        QualitySettings.SetQualityLevel(selectedGraphicsLevel);
        PlayerPrefs.SetInt("savedQualityLevel", selectedGraphicsLevel);
    }


     public void GraphicsLeft()
    {
        selectedGraphicsLevel--;
        if(selectedGraphicsLevel < 0)
        {
            selectedGraphicsLevel = 0;
        }
        UpdateGraphicsLabel();
    }

    public void GraphicsRight()
    {
        selectedGraphicsLevel++;
        if (selectedGraphicsLevel > 2 )
        {
            selectedGraphicsLevel = 2;
        }
      UpdateGraphicsLabel();
    }

    public void UpdateGraphicsLabel()
    {
        int currentQualityLevel;
        currentQualityLevel = QualitySettings.GetQualityLevel();
        Debug.Log(currentQualityLevel);
        if (selectedGraphicsLevel ==1){
            graphichsLevelLabel.text = "Medium";
        }
         if (selectedGraphicsLevel ==0){
            graphichsLevelLabel.text = "Low";
        }
         if (selectedGraphicsLevel ==2){
            graphichsLevelLabel.text = "High";
        }
    }

    public void ResLeft()
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }
        UpdateResolutionLabel();
    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count -1 )
        {
            selectedResolution = resolutions.Count - 1;
        }
        UpdateResolutionLabel();
    }

    public void UpdateResolutionLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x "  + resolutions[selectedResolution].vertical.ToString();
    }


    public void setMasterVol()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVol", masterSlider.value); //UKLADANI NASTAVENI HLASITOSTI U UZIVATELE
    }

    public void SetMusicVol()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }
    public void SetSFXVol()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SfxVol", sfxSlider.value);
        PlayerPrefs.SetFloat("SfxVol", sfxSlider.value);
    }


}

[System.Serializable]  //aby to bylo viditelny v hieraarchii
public class ResItem  //custom rozliseni
{
    public int horizontal;
    public int vertical;

}

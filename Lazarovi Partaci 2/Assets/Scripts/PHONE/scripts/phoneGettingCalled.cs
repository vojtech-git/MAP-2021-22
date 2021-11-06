using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phoneGettingCalled : MonoBehaviour
{
    [Header("Pozadí")]
    public GameObject callingBG;
    
    [Header("Stránky")]
    public GameObject caller;
    public GameObject calledPerson;
    public GameObject home;

    GameObject wallpaper;
    GameObject ringtone;

    public callTimer callTimer;

    void Start()
    {
        ringtone = GameObject.FindGameObjectWithTag("Ringtone");
        wallpaper = GameObject.FindGameObjectWithTag("Wallpaper");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && home.activeSelf)
        {
            Start();

            wallpaper.SetActive(false);
            callingBG.SetActive(true);

            home.SetActive(false);
            caller.SetActive(true);

            ringtone.GetComponent<AudioSource>().Play();
            ringtone.GetComponent<AudioSource>().loop = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && caller.activeSelf)
        {
            callingBG.SetActive(false);
            wallpaper.SetActive(true);

            home.SetActive(true);
            caller.SetActive(false);

            ringtone.GetComponent<AudioSource>().Stop();
            ringtone.GetComponent<AudioSource>().loop = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) && caller.activeSelf)
        {
            calledPerson.SetActive(true);
            caller.SetActive(false);

            ringtone.GetComponent<AudioSource>().Stop();
            ringtone.GetComponent<AudioSource>().loop = false;

            callTimer.beginTimer();
        }
        if (Input.GetKeyDown(KeyCode.E) && calledPerson.activeSelf)
        {
            callTimer.endTimer();

            callingBG.SetActive(false);
            wallpaper.SetActive(true);

            home.SetActive(true);
            calledPerson.SetActive(false);
        }
        //not fully working yet
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phoneGettingCalled : MonoBehaviour
{
    [Header("Pozadí")]
    public GameObject callingBG1;
    public GameObject callingBG2;
    
    [Header("Stránky")]
    public GameObject caller1;
    public GameObject caller2;
    public GameObject calledPerson;
    public GameObject home;

    public GameObject[] wallpapers;
    GameObject ringtone;

    //public GameObject voicline;

    public AudioSource[] storyVoicelines;
    public AudioSource[] storyVoicelines2;

    public phoneOnOff phoneOnOff;

    public callTimer callTimer;

    void Start()
    {
        ringtone = GameObject.FindGameObjectWithTag("Ringtone");
    }

    void Update()
    {
        /*
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
        */ /*
        if (Input.GetKeyDown(KeyCode.E) && caller.activeSelf)
        {
            callingBG.SetActive(false);
            wallpaper.SetActive(true);

            home.SetActive(true);
            caller.SetActive(false);

            ringtone.GetComponent<AudioSource>().Stop();
            ringtone.GetComponent<AudioSource>().loop = false;
        }
        */
        if (Input.GetKeyDown(KeyCode.Return) && caller1.activeSelf)
        {
            calledPerson.SetActive(true);
            caller1.SetActive(false);

            ringtone.GetComponent<AudioSource>().Stop();
            ringtone.GetComponent<AudioSource>().loop = false;

            callTimer.beginTimer();

            StartCoroutine(test());
        }
        /*
        if (Input.GetKeyDown(KeyCode.E) && calledPerson.activeSelf)
        {
            callTimer.endTimer();

            callingBG.SetActive(false);
            wallpaper.SetActive(true);

            home.SetActive(true);
            calledPerson.SetActive(false);
        }
        //not fully working yet
        */
    }

    public void storyGettingCalled()
    {
        Start();

        wallpapers[0].SetActive(false);
        callingBG1.SetActive(true);

        home.SetActive(false);
        caller1.SetActive(true);

        ringtone.GetComponent<AudioSource>().Play();
        ringtone.GetComponent<AudioSource>().loop = true;
    }

    public void StoryGettingCalled2()
    {
        Start();

        wallpapers[0].SetActive(false);
        home.SetActive(false);
        callingBG2.SetActive(true);
        caller2.SetActive(true);

        ringtone.GetComponent<AudioSource>().Play();
        ringtone.GetComponent<AudioSource>().loop = true;
    }

    IEnumerator test(AudioSource[] audioVoicelines)
    {
        yield return new WaitForSecondsRealtime(1f);

        //voicline.GetComponent<AudioSource>().Play();

        for (int i = 0; i < storyVoicelines2.Length; i++)
        {
            if (audioVoicelines[i] != null)
            {
                if (audioVoicelines[i].isPlaying)
                {
                    audioVoicelines[i].Stop();
                }

                audioVoicelines[i].Play();
                yield return new WaitUntil(() => !audioVoicelines[i].isPlaying); // wait untill musi pøijmout func jako parametr. proto vytvaøím anonym metodu
            }
        }

        // vojtech quest logika, tuhle metodu spustit az potom co se prehrajou hlasky
        QuestingManager.OnPointGained(GoalType.PhoneCall, 0);

        yield return new WaitForSecondsRealtime(5f);
        
        phoneOnOff.turnOff();
        phoneOnOff.xdLmaoJanProchazka();

        yield return new WaitForSecondsRealtime(1f);

        //callTimer.endTimer();

        //callingBG.SetActive(false);
        //wallpaper.SetActive(true);

        //calledPerson.SetActive(false);
        //home.SetActive(true);
    }
}

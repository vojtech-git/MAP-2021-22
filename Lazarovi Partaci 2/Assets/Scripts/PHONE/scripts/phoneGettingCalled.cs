using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phoneGettingCalled : MonoBehaviour
{
    [Header("Pozadí")]
    public GameObject callingBG;
    
    [Header("Stránky")]
    public GameObject caller1;
    public GameObject caller2;
    public GameObject calledPerson1;
    public GameObject calledPerson2;
    public GameObject calledPerson3;
    public GameObject home;

    public GameObject[] wallpapers;
    GameObject currentWallpaper;
    GameObject ringtone;

    //public GameObject voicline;

    public AudioSource[] storyVoicelines1;
    public AudioSource[] storyVoicelines2;
    public AudioSource[] storyVoicelines3;

    public phoneOnOff phoneOnOff;

    public callTimer callTimer;
    public callTimer callTimer2;
    public callTimer callTimer3;

    void Start()
    {
        ringtone = GameObject.FindGameObjectWithTag("Ringtone");

        foreach (GameObject wallpaper in wallpapers)
        {
            if (wallpaper.activeSelf)
            {
                currentWallpaper = wallpaper;
            }
        }
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
        if (caller1 != null)
        {
            if (Input.GetKeyDown(KeyCode.Return) && caller1.activeSelf)
            {
                caller1.SetActive(false);

                calledPerson1.SetActive(true);

                ringtone.GetComponent<AudioSource>().Stop();
                ringtone.GetComponent<AudioSource>().loop = false;

                callTimer.beginTimer();

                Debug.Log("prijmuti hovoru");

                foreach (AudioSource audioSource in storyVoicelines1)
                {
                    Debug.Log("voiceline");
                }

                StartCoroutine(test(storyVoicelines1));
            } 
        }
        else
        {
            Debug.Log("caller1 neni specifikovan");
        }

        if (caller2 != null)
        {
            if (Input.GetKeyDown(KeyCode.Return) && caller2.activeSelf)
            {
                caller2.SetActive(false);

                calledPerson2.SetActive(true);

                ringtone.GetComponent<AudioSource>().Stop();
                ringtone.GetComponent<AudioSource>().loop = false;

                callTimer2.beginTimer();

                StartCoroutine(test(storyVoicelines2));
            } 
        }
        else
        {
            Debug.Log("caller 2 neni specifikovan");
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

    public void StoryGettingCalled1()
    {
        Start();

        currentWallpaper.SetActive(false);
        home.SetActive(false);
        
        callingBG.SetActive(true);
        caller1.SetActive(true);

        ringtone.GetComponent<AudioSource>().Play();
        ringtone.GetComponent<AudioSource>().loop = true;
    }

    public void StoryGettingCalled2()
    {
        Start();

        currentWallpaper.SetActive(false);
        home.SetActive(false);

        callingBG.SetActive(true);
        caller2.SetActive(true);

        ringtone.GetComponent<AudioSource>().Play();
        ringtone.GetComponent<AudioSource>().loop = true;
    }
    public void StoryGettingCalled3()
    {
        Start();

        currentWallpaper.SetActive(false);
        home.SetActive(false);

        callingBG.SetActive(true);
        calledPerson3.SetActive(true);

        callTimer3.beginTimer();

        StartCoroutine(test(storyVoicelines3));
    }

    IEnumerator test(AudioSource[] audioVoicelines)
    {
        yield return new WaitForSecondsRealtime(1f);

        //voicline.GetComponent<AudioSource>().Play();

        for (int i = 0; i < audioVoicelines.Length; i++)
        {
            if (audioVoicelines[i] != null)
            {
                if (audioVoicelines[i].isPlaying)
                {
                    audioVoicelines[i].Stop();
                }

                audioVoicelines[i].Play();
                yield return new WaitUntil(() => !audioVoicelines[i].isPlaying); // wait untill musi pøijmout func jako parametr. proto vytvaøím anonym metodu

                Debug.Log("prehrana hlasla " + i);
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

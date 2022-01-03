using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [Header("Start Of Game Quest")]
    public Quest startGameQuest;

    [Header("Player")]
    public GameObject FPS;

    [Header("In Scene Quest UI")]
    public VerticalLayoutGroup questVerticalLayout;
    public GameObject questTitlePrefab;
    public GameObject goalDescriptionPrefab;
    public GameObject descriptionsLayoutPrefab;

    public Text popupText;
    public Text secondaryPopupText;
    public Text acceptQuestText;
    [Header("Quest Menu UI")]
    public GameObject questSelectionMenu;
    public HorizontalLayoutGroup questLayout;
    public GameObject questTabButtonPrefab;
    public Text NoQuestAvailable;
    public GameObject QuestAcceptButton;

    private static GameStateManager _instance;

    public static GameStateManager Instance { get { return _instance; } }


    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        if (QuestingSystem.isStartOfGame && SceneManager.GetActiveScene().name != "splash screen" && SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Main Menu")
        {
            QuestingSystem.AcceptQuest(startGameQuest);

            QuestingSystem.isStartOfGame = false;
        }
        FPS = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnLevelWasLoaded(int level)
    {
        FPS = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(FPS);
    }

    public void PlayAudioMethod(AudioSource[] audioSources, int speakerID = -1)
    {
        StartCoroutine(PlayAudio(audioSources, speakerID));
    }

    public void ShowMessageFor5Sec(string message, int priority)
    {
        if (priority == 1)
        {
            StartCoroutine(PopupUI(popupText, message));
        }
        if (priority == 2)
        {
            StartCoroutine(PopupUI(secondaryPopupText, message));
        }
    }

    private IEnumerator PopupUI(Text popUpText, string popUpMessage) // protoze pouzivam stejnej ui element. popUpText.text se nastavi na to co chci pri prvni corutinì ale pak rovnou zaène druhá corutina ktera sice pøepíše text ale potom ji to ta prvni corutina zase pøepíše na "". 
    {                                                                // asi neni problem
        popUpText.text = popUpMessage;

        yield return new WaitForSecondsRealtime(5);

        popUpText.text = "";
    }

    public IEnumerator PlayAudio(AudioSource[] audioSources, int speakerID)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] != null)
            {
                if (audioSources[i].isPlaying)
                {
                    audioSources[i].Stop();
                }

                audioSources[i].Play();
                yield return new WaitUntil(() => !audioSources[i].isPlaying); // wait untill musi pøijmout func jako parametr. proto vytvaøím anonym metodu
            }
        }

        QuestingSystem.ProgressQuests(GoalType.Talk, speakerID);
    }
}



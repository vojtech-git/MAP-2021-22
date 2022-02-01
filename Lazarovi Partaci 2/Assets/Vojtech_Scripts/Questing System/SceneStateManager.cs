using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneStateManager : MonoBehaviour
{
    public QuestlineSO storyQuestline;
    static bool startOfGame = true;

    [Header("Quest prefabs")]
    public GameObject questTitlePrefab;
    public GameObject goalDescriptionPrefab;
    public GameObject descriptionsLayoutPrefab;
    [Header("Quest ui parent")]
    public VerticalLayoutGroup questVerticalLayout;
    [Header("Quest menu prefabs")]
    public GameObject questTabButtonPrefab;
    public GameObject acceptButtonPrefab;
    [Header("Quest menu parents")]
    public GameObject questSelectionMenu;
    public HorizontalLayoutGroup questLayout;
    public VerticalLayoutGroup qDescriptionLayout;
    [Header("Other")]
    public Text popupText;
    public Text secondaryPopupText;
    public Text acceptQuestText;

    private static SceneStateManager instance;
    public static SceneStateManager Instance { get { return instance; } }

    private void Awake()
    {
        //if (_instance != null && _instance != this)
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    _instance = this;
        //}

        questVerticalLayout = GameObject.Find("Quest_Vertical_Layout").GetComponent<VerticalLayoutGroup>();

        instance = this;
    }

    private void Start()
    {
        foreach (StoryObject storyObject in GameObject.FindObjectsOfType<StoryObject>())
        {
            storyObject.ApplySaveData();
        }

        if (startOfGame)
        {
            //Debug.Log("sceneStateManager starting the game");

            QuestingManager.StartQuestline(storyQuestline.questline);
            startOfGame = false;
        }
    }


    public void PlayAudioMethod(AudioSource[] audioSources)
    {
        StartCoroutine(PlayAudio(audioSources));
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

    public IEnumerator PlayAudio(AudioSource[] audioSources)
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
    }
}



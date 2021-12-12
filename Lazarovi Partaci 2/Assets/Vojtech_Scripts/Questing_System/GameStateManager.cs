using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject FPS;

    [Header("Other")]
    public GameObject questTitlePrefab;
    public GameObject goalDescriptionPrefab;
    public GameObject descriptionsLayoutPrefab;
    public GameObject questTabButtonPrefab;
    public GameObject acceptButtonPrefab;
    public GameObject questSelectionMenu;
    public VerticalLayoutGroup questVerticalLayout;
    public HorizontalLayoutGroup questLayout;
    public VerticalLayoutGroup qDescriptionLayout;
    public Text popupText;
    public Text secondaryPopupText;
    public Text acceptQuestText;

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
        }
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
            if (audioSources[i].isPlaying)
            {
                audioSources[i].Stop();
            }

            if (audioSources[i] != null)
            {
                audioSources[i].Play();
                yield return new WaitUntil(() => !audioSources[i].isPlaying); // wait untill musi pøijmout func jako parametr. proto vytvaøím anonym metodu
            }
        }
    }
}



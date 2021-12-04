using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject questTitlePrefab;
    public GameObject goalDescriptionPrefab;
    public GameObject descriptionsLayoutPrefab;
    public VerticalLayoutGroup questVerticalLayout;
    public Text popupText;
    public Text secondaryPopupText;
    public Text acceptQuestText;

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
}



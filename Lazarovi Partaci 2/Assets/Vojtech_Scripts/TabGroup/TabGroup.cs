using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<UiTabButton> tabButtons;
    public UiTabButton selectedButton;
    public List<GameObject> objectsToSwap;

    public void Subscribe(UiTabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<UiTabButton>();
        }

        tabButtons.Add(button);
    }
    public void UnsbscribeAll()
    {
        tabButtons.Clear();
    }

    public void OnTabEnter(UiTabButton uiTabButton)
    {
        ResetTabs();
        if (selectedButton == null || selectedButton != uiTabButton)
        {
            uiTabButton.uiText.fontStyle = FontStyle.Italic;
        }
    }

    public void OnTabExit(UiTabButton uiTabButton)
    {
        ResetTabs();
    }

    public virtual void OnTabSelected(UiTabButton uiTabButton)
    {
        //if (selectedButton != null)
        //{
        //    selectedButton.Deselect();
        //}

        selectedButton = uiTabButton;

        GetComponentInParent<SelectedQuest>().selectedQuest = uiTabButton.quest;

        //uiTabButton.Select();

        ResetTabs();
        uiTabButton.uiText.fontStyle = FontStyle.Bold;

        int index = uiTabButton.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (UiTabButton button in tabButtons)
        {
            if (selectedButton != null && selectedButton == button)
                continue;
            button.uiText.fontStyle = FontStyle.Normal;
        }
    }
}

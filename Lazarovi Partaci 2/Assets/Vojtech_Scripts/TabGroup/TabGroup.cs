using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [Header("UI text to write quest info into")]
    public Text goalDescriptionsText;
    public Text questDetailsText;

    [Header("code things")]
    public List<UiTabButton> tabButtons;
    public UiTabButton selectedButton;

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

        goalDescriptionsText.text = "";
        questDetailsText.text = "";
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
        selectedButton = uiTabButton; 
        uiTabButton.uiText.fontStyle = FontStyle.Bold;

        ResetTabs();

        goalDescriptionsText.text = CreateGoalsDescriptionsString(uiTabButton.quest);
        questDetailsText.text = uiTabButton.quest.details;        
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

    public void AcceptSelectedQuest()
    {
        QuestingSystem.AcceptQuest(selectedButton.quest);


        tabButtons.Remove(selectedButton);
        Destroy(selectedButton.gameObject);
        selectedButton = null;

        if (tabButtons.Count == 0)
        {
            QuestingSystem.CloseQuestMenu();
        }
        else
        {
            OnTabSelected(tabButtons[0]);
        }
    }

    public string CreateGoalsDescriptionsString(Quest quest)
    {
        string finalString = "";
        foreach (Goal goal in quest.questGoals)
        {
            string tempString = $"{goal.goalDescription}";

            if (goal.goalType != GoalType.Reach || goal.goalType != GoalType.Talk)
            {
                tempString += $" - poèet: {goal.goalValue}";
            }
            tempString += "\n";

            finalString += tempString;
        }

        return finalString;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest
{
    public bool isMainStoryLine;
    public string title;
    public QuestStyle questStyle;
    public List<QuestGoal> activeQuestGoals;

    [Header("UI")]
    public Text uiQuestTitle;
    public VerticalLayoutGroup descriptionsLayoutGroup;

    public bool IsComplete()
    {
        foreach (QuestGoal questGoal in activeQuestGoals)
        {
            if (!questGoal.IsComplete())
                return false;
        }
        return true;
    }

    public void AddPoint(GoalType _goalType, int _itemID)
    {
        List<QuestGoal> goalsToComplete = new List<QuestGoal>();

        if (questStyle == QuestStyle.AfterEachOther)
        {
            if (_goalType == activeQuestGoals[0].goalType && _itemID == activeQuestGoals[0].itemID) // pro prvni questGoal v øadì questGoalù, pokid se shoduje itemId a questType
            {
                activeQuestGoals[0].AddPoint(); // Pøidej bodu do prvniho goalu

                if (activeQuestGoals[0].IsComplete()) // pokud je po pøidani pointu completed  
                {
                    QuestingSystem.uiManager.ShowMessageFor5Sec($"èast úkolu {activeQuestGoals[0].goalDescription} byla splnìna", 2);
                    goalsToComplete.Add(activeQuestGoals[0]); // pøidej ho do goals to complete

                    if (activeQuestGoals.Count > 1) // pokud existuje dalsi questGoal
                    {
                        activeQuestGoals[1].uiGoalDescription = GameObject.Instantiate(QuestingSystem.uiManager.goalDescriptionPrefab, descriptionsLayoutGroup.transform).GetComponent<Text>(); //vytvoø ui reprezentaci pro dalsi questGoal
                        activeQuestGoals[1].PutValuesOnUi(); // napis currentValue do QGoal ui
                    }
                }
            }
        }
        else if (questStyle == QuestStyle.AtTheSameTime)
        {
            foreach (QuestGoal goal in activeQuestGoals)
            {
                if (_goalType == goal.goalType && _itemID == goal.itemID)
                {
                    goal.AddPoint();

                    if (goal.IsComplete())
                    {
                        QuestingSystem.uiManager.ShowMessageFor5Sec($"èast úkolu {goal.goalDescription} byla splnìna", 2);
                        goalsToComplete.Add(goal);
                    }
                }
            }
        }

        foreach (QuestGoal goalToComplete in goalsToComplete)
        {
            GameObject.Destroy(goalToComplete.uiGoalDescription.gameObject);
            activeQuestGoals.Remove(goalToComplete);
            if (activeQuestGoals.Count == 0)
            {
                GameObject.Destroy(descriptionsLayoutGroup);
            }
        }
    }

    public void InstatiateQuestUI()
    {
        //instanciace pro Questy
        descriptionsLayoutGroup = GameObject.Instantiate(QuestingSystem.uiManager.descriptionsLayoutPrefab, QuestingSystem.uiManager.questVerticalLayout.transform).GetComponentInChildren<VerticalLayoutGroup>();

        uiQuestTitle = GameObject.Instantiate(QuestingSystem.uiManager.questTitlePrefab, descriptionsLayoutGroup.transform).GetComponent<Text>();
        uiQuestTitle.GetComponent<Text>().text = title;

        // instanciace pro questGoaly
        if (questStyle == QuestStyle.AfterEachOther)
        {
            activeQuestGoals[0].uiGoalDescription = GameObject.Instantiate(QuestingSystem.uiManager.goalDescriptionPrefab, descriptionsLayoutGroup.transform).GetComponent<Text>();
            activeQuestGoals[0].PutValuesOnUi();
        }
        else if (questStyle == QuestStyle.AtTheSameTime)
        {
            foreach (QuestGoal questGoal in activeQuestGoals)
            {
                questGoal.uiGoalDescription = GameObject.Instantiate(QuestingSystem.uiManager.goalDescriptionPrefab, descriptionsLayoutGroup.transform).GetComponent<Text>();
                questGoal.PutValuesOnUi();
            }
        }
    }
}

public enum QuestStyle
{
    AfterEachOther,
    AtTheSameTime
}
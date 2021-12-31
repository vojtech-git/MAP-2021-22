using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest
{
    public string title;
    public bool isMainStoryLine;
    public QuestStyle questStyle;
    public List<Goal> questGoals;
    List<Goal> completedGoals = new List<Goal>();
    [HideInInspector]
    public bool completed = false;

    [Header("UI")]
    private Text uiQuestTitle;
    private VerticalLayoutGroup descriptionsLayoutGroup;

    public void Accept()
    {
        GameStateManager.Instance.ShowMessageFor5Sec($"úkol {title} byl pøijat", 1);

        InstatiateQuestUI();

        if (questStyle == QuestStyle.AfterEachOther)
        {
            questGoals[0].Accept(descriptionsLayoutGroup.transform);
        }
        else if (questStyle == QuestStyle.AtTheSameTime)
        {
            foreach (Goal questGoalToAccept in questGoals)
            {
                questGoalToAccept.Accept(descriptionsLayoutGroup.transform);
            }
        }

        QuestingSystem.OnQuestAccept(title);
    }

    public void AddPoint(GoalType _goalType, int _itemID)
    {
        List<Goal> goalsToComplete = new List<Goal>();

        if (questStyle == QuestStyle.AfterEachOther)
        {
            if (_goalType == questGoals[0].goalType && _itemID == questGoals[0].itemID) // pro prvni questGoal v øadì questGoalù, pokid se shoduje itemId a questType
            {
                questGoals[0].AddPoint(); // Pøidej bodu do prvniho goalu

                if (questGoals[0].IsComplete()) // pokud je po pøidani pointu completed  
                {
                    questGoals[0].Complete(); // spust jeho complete()
                    goalsToComplete.Add(questGoals[0]); // pøidej ho do goals to complete

                    if (questGoals.Count > 1) // pokud existuje dalsi questGoal
                    {
                        questGoals[1].Accept(descriptionsLayoutGroup.transform);
                    }
                }
            }
        }
        else if (questStyle == QuestStyle.AtTheSameTime)
        {
            foreach (Goal goal in questGoals)
            {
                if (_goalType == goal.goalType && _itemID == goal.itemID)
                {
                    goal.AddPoint();

                    if (goal.IsComplete())
                    {
                        goal.Complete();
                        goalsToComplete.Add(goal);
                    }
                }
            }
        }

        CompleteQuestGoals(goalsToComplete);
    }

    public void Complete()
    {
        GameStateManager.Instance.ShowMessageFor5Sec($"Úkol {title} byl splnìn", 1);
        GameObject.Destroy(uiQuestTitle);
        GameObject.Destroy(descriptionsLayoutGroup);
        completed = true;
        QuestingSystem.OnQuestComplete(title);
    }

    public bool IsComplete()
    {
        foreach (Goal questGoal in questGoals)
        {
            if (!questGoal.IsComplete())
                return false;
        }
        return true;
    }

    void CompleteQuestGoals(List<Goal> questGoalsToComplete)
    {
        foreach (Goal questGoal in questGoalsToComplete)
        {
            questGoals.Remove(questGoal);
            completedGoals.Add(questGoal);
            questGoal.Complete();
        }

        questGoalsToComplete.Clear();
    }

    void InstatiateQuestUI()
    {
        //instanciace pro Questy
        descriptionsLayoutGroup = GameObject.Instantiate(GameStateManager.Instance.descriptionsLayoutPrefab, GameStateManager.Instance.questVerticalLayout.transform).GetComponentInChildren<VerticalLayoutGroup>();

        uiQuestTitle = GameObject.Instantiate(GameStateManager.Instance.questTitlePrefab, descriptionsLayoutGroup.transform).GetComponent<Text>();
        uiQuestTitle.GetComponent<Text>().text = title;

        #region instanciace pro questGoaly 
        //// instaciace pro goaly je v goalech v jejich accept
        //if (questStyle == QuestStyle.AfterEachOther)
        //{
        //    activeQuestGoals[0].uiGoalDescription = GameObject.Instantiate(UIManager.Instance.goalDescriptionPrefab, descriptionsLayoutGroup.transform).GetComponent<Text>();
        //    activeQuestGoals[0].PutValuesOnUi();
        //}
        //else if (questStyle == QuestStyle.AtTheSameTime)
        //{
        //    foreach (QuestGoal questGoal in activeQuestGoals)
        //    {
        //        questGoal.uiGoalDescription = GameObject.Instantiate(UIManager.Instance.goalDescriptionPrefab, descriptionsLayoutGroup.transform).GetComponent<Text>();
        //        questGoal.PutValuesOnUi();
        //    }
        //}
        #endregion
    }
}

public enum QuestStyle
{
    AfterEachOther,
    AtTheSameTime
}
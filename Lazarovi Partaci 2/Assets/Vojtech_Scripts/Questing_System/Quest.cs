using System;
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
    List<QuestGoal> completedGoals = new List<QuestGoal>();
    public bool completed = false;

    [Header("Zvuk")]
    public AudioSource[] acceptQuestAudio;
    public AudioSource[] completeQuestAudio;

    [Header("UI")]
    private Text uiQuestTitle;
    private VerticalLayoutGroup descriptionsLayoutGroup;

    public void Accept()
    {
        GameStateManager.Instance.ShowMessageFor5Sec($"úkol {title} byl pøijat", 1);

        InstatiateQuestUI();

        if (questStyle == QuestStyle.AfterEachOther)
        {
            activeQuestGoals[0].Accept(descriptionsLayoutGroup.transform);
        }
        else if (questStyle == QuestStyle.AtTheSameTime)
        {
            foreach (QuestGoal questGoalToAccept in activeQuestGoals)
            {
                questGoalToAccept.Accept(descriptionsLayoutGroup.transform);
            }
        }

        if (acceptQuestAudio != null)
            GameStateManager.Instance.StartCoroutine(GameStateManager.Instance.PlayAudio(acceptQuestAudio));
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
                    activeQuestGoals[0].Complete(); // spust jeho complete()
                    goalsToComplete.Add(activeQuestGoals[0]); // pøidej ho do goals to complete

                    if (activeQuestGoals.Count > 1) // pokud existuje dalsi questGoal
                    {
                        activeQuestGoals[1].Accept(descriptionsLayoutGroup.transform);
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
        if (completeQuestAudio != null)
            GameStateManager.Instance.StartCoroutine(GameStateManager.Instance.PlayAudio(completeQuestAudio));

        GameStateManager.Instance.ShowMessageFor5Sec($"Úkol {title} byl splnìn", 1);

        //event handler OnQuestComplete
        GameObject.Destroy(uiQuestTitle);
        GameObject.Destroy(descriptionsLayoutGroup);

        completed = true;
    }

    public bool IsComplete()
    {
        foreach (QuestGoal questGoal in activeQuestGoals)
        {
            if (!questGoal.IsComplete())
                return false;
        }
        return true;
    }

    void CompleteQuestGoals(List<QuestGoal> questGoalsToComplete)
    {
        foreach (QuestGoal questGoal in questGoalsToComplete)
        {
            activeQuestGoals.Remove(questGoal);
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
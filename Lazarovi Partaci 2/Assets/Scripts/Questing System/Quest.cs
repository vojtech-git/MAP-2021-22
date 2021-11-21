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
    public event EventHandler OnQuestCompleted;


    [Header("UI")]
    public Text uiQuestTitle;
    public VerticalLayoutGroup descriptionsLayoutGroup;

    public void Accept()
    {
        QuestingSystem.uiManager.ShowMessageFor5Sec($"úkol {title} byl pøijat", 1);
        
        InstatiateQuestUI();

        if (questStyle == QuestStyle.AfterEachOther)
        {
            activeQuestGoals[0].Accept();
        }
        else if (questStyle == QuestStyle.AtTheSameTime)
        {
            foreach (QuestGoal questGoalToAccept in activeQuestGoals)
            {
                questGoalToAccept.Accept();
            }
        }

        //on accept quest eventHandler
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

                    if (activeQuestGoals.Count > 1) // pokud existuje dalsi questGoal accepti a udelej mu ui
                    {
                        activeQuestGoals[1].Accept();
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

        QuestingSystem.uiManager.ShowMessageFor5Sec($"Úkol {title} byl splnìn", 1);

        //event handler OnQuestComplete
        GameObject.Destroy(uiQuestTitle);
        GameObject.Destroy(descriptionsLayoutGroup);
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

    //void CompleteGoal(QuestGoal goalToComplete)
    //{
    //    GameObject.Destroy(goalToComplete.uiGoalDescription.gameObject);

    //    activeQuestGoals.Remove(goalToComplete);
    //    completedGoals.Add(goalToComplete);
    //}

    public bool IsComplete()
    {
        foreach (QuestGoal questGoal in activeQuestGoals)
        {
            if (!questGoal.IsComplete())
                return false;
        }
        return true;
    }

    //IEnumerator PlayAudio(AudioSource[] audioSources)
    //{
    //    for (int i = 0; i < audioSources.Length; i++)
    //    {
    //        audioSources[i].Play();
    //        yield return new WaitUntil(() => !audioSources[i].isPlaying); // wait untill musi pøijmout func jako parametr. proto vytvaøím anonym metodu
    //    }
    //}
}

public enum QuestStyle
{
    AfterEachOther,
    AtTheSameTime
}
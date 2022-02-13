using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest
{
    public string title;
    public QuestStyle questStyle;
    public List<Goal> activeGoals;
    [HideInInspector]
    public List<Goal> completedGoals = new List<Goal>();

    bool completed;
    public bool Completed
    {
        get
        {
            if (activeGoals.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private set { completed = value; }
    }

    private Text uiTitle;
    private VerticalLayoutGroup uiParent;

    public Quest(string _title, QuestStyle _questStyle, List<Goal> _activeQuestGoals)
    {
        title = _title;
        questStyle = _questStyle;
        activeGoals = _activeQuestGoals;
    }

    public void StartQuest()
    {
        QuestingManager.OnQuestStart(this);

        InstatiateQuestUI();

        if (questStyle == QuestStyle.AfterEachOther)
        {
            activeGoals[0].StartGoal(uiParent.transform);
        }
        else if (questStyle == QuestStyle.AtTheSameTime)
        {
            foreach (Goal questGoalToAccept in activeGoals)
            {
                questGoalToAccept.StartGoal(uiParent.transform);
            }
        }
    }

    public void ProgressQuest(GoalType _goalType, int _itemID)
    {
        if (questStyle == QuestStyle.AfterEachOther)
        {
            if (_goalType == activeGoals[0].goalType && _itemID == activeGoals[0].itemId) // pro prvni questGoal v øadì questGoalù, pokid se shoduje itemId a questType
            {
                activeGoals[0].ProgressGoal(); // Pøidej bodu do prvniho goalu

                if (activeGoals[0].Completed) // pokud je po pøidani pointu completed  
                {
                    MoveGoalToCompleted(activeGoals[0]); // pøidej ho do goals to complete

                    if (activeGoals.Count >= 1) // pokud existuje dalsi questGoal
                    {
                        activeGoals[0].StartGoal(uiParent.transform); // acceptni ho
                    }
                }
            }
        }
        else if (questStyle == QuestStyle.AtTheSameTime)
        {
            List<Goal> goalsToComplete = new List<Goal>();

            foreach (Goal goal in activeGoals)
            {
                if (_goalType == goal.goalType && _itemID == goal.itemId)
                {
                    goal.ProgressGoal();

                    if (goal.Completed)
                        goalsToComplete.Add(goal);
                }
            }

            // nemuzu odebírat goaly z kolekce kdyz tou kolekcí procházím foreachem
            foreach (Goal goalToComplete in goalsToComplete)
                MoveGoalToCompleted(goalToComplete);
        }

        if (Completed)
            CompleteQuest();
    }

    public void CompleteQuest()
    {
        QuestingManager.OnQuestCompleted(this);

        GameObject.Destroy(uiTitle);
        GameObject.Destroy(uiParent);
    }

    void MoveGoalToCompleted(Goal goal)
    {
        activeGoals.Remove(goal);
        completedGoals.Add(goal);
    }
    void InstatiateQuestUI()
    {
        uiParent = GameObject.Instantiate(QuestCanvas.Instance.descriptionsLayoutPrefab, QuestCanvas.Instance.questVerticalLayout.transform).GetComponentInChildren<VerticalLayoutGroup>();

        uiTitle = GameObject.Instantiate(QuestCanvas.Instance.questTitlePrefab, uiParent.transform).GetComponent<Text>();
        uiTitle.text = title;
    }
}

public enum QuestStyle
{
    AfterEachOther,
    AtTheSameTime
}
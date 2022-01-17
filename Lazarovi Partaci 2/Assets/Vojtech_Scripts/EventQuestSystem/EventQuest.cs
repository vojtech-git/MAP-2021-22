using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EventQuest : MonoBehaviour
{
    public string title;
    public QuestStyle questStyle;
    public List<EventGoal> questGoals;
    bool completed = false;
    public string nextQuest;

    List<EventGoal> completedGoals = new List<EventGoal>();

    [Header("UI")]
    private Text uiTitle;
    private VerticalLayoutGroup uiParent;

    private void Start()
    {
        EventQuestingManager.onQuestStarted += OnQuestStarted;
        EventQuestingManager.onQuestProgressed += OnQuestProgressed;
        EventQuestingManager.onQuestCompleted += OnQuestCompleted;

        EventQuestingManager.onPointGained += ProgressQuest;
    }
    private void OnDestroy()
    {
        EventQuestingManager.onQuestStarted -= OnQuestStarted;
        EventQuestingManager.onQuestProgressed -= OnQuestProgressed;
        EventQuestingManager.onQuestCompleted -= OnQuestCompleted; 
        
        EventQuestingManager.onPointGained -= ProgressQuest;
    }

    public void StartQuest()
    {
        Debug.Log("StartQuest method triggeted " + name);

        EventQuestingManager.OnQuestStarted(this);

        // instanciate quest ui
        uiParent = Instantiate(EventQuestingManager.Instance.questParentPrefab, EventQuestingManager.Instance.questUiParent.transform).GetComponent<VerticalLayoutGroup>();
        uiTitle = Instantiate(EventQuestingManager.Instance.questTitlePrefab, uiParent.transform).GetComponent<Text>();
        uiTitle.text = title;

        if (questStyle == QuestStyle.AfterEachOther)
        {
            questGoals[0].StartGoal(uiParent.transform);
        }
        else if (questStyle == QuestStyle.AtTheSameTime)
        {
            foreach (EventGoal questGoalToAccept in questGoals)
            {
                questGoalToAccept.StartGoal(uiParent.transform);
            }
        }
    }
    void ProgressQuest(int id, GoalType goalType)
    {
        EventQuestingManager.OnQuestProgressed(this);

        List<EventGoal> goalsToComplete = new List<EventGoal>();

        if (questStyle == QuestStyle.AfterEachOther)
        {
            if (goalType == questGoals[0].goalType && id == questGoals[0].itemId) // pro prvni questGoal v øadì questGoalù, pokid se shoduje itemId a questType
            {
                questGoals[0].CurrentValue++; // Pøidej bodu do prvniho goalu

                if (questGoals[0].Complete) // pokud je po pøidani pointu completed  
                {
                    goalsToComplete.Add(questGoals[0]); // pøidej ho do goals to complete

                    if (questGoals.Count > 1) // pokud existuje dalsi questGoal
                    {
                        questGoals[1].StartGoal(uiParent.transform);
                    }
                }
            }
        }
        else if (questStyle == QuestStyle.AtTheSameTime)
        {
            foreach (EventGoal goal in questGoals)
            {
                if (goalType == goal.goalType && id == goal.itemId)
                {
                    goal.CurrentValue++;

                    if (goal.Complete)
                    {
                        goalsToComplete.Add(goal);
                    }
                }
            }
        }

        CompleteQuestGoals(goalsToComplete);
    }
    void CompleteQuestGoals(List<EventGoal> questGoalsToComplete)
    {
        foreach (EventGoal questGoal in questGoalsToComplete)
        {
            questGoals.Remove(questGoal);
            completedGoals.Add(questGoal);
        }
        if (questGoals.Count == 0)
        {
            CompleteQuest();
        }
    }
    void CompleteQuest()
    {
        if (completed == false)
        {
            EventQuestingManager.OnQuestCompleted(this, nextQuest);

            GameObject.Destroy(uiTitle);
            GameObject.Destroy(uiParent);
            completed = true; 
        }
    }

    void OnQuestStarted(EventQuest quest)
    {

    }
    void OnQuestProgressed(EventQuest quest)
    {

    }
    void OnQuestCompleted(EventQuest quest, string nextQuest)
    {
        if (nextQuest == this.title)
        {
            StartQuest();
        }
    }
}

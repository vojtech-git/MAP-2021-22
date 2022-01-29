using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class QuestingManager
{
    public static Action<Quest> onQuestStarted;
    public static Action<Quest> onQuestCompleted;
    public static Action<Goal> onGoalStarted;
    public static Action<Goal> onGoalCompleted;
    public static Action<GoalType, int> onPointGained;

    public static void OnQuestStart(Quest quest)
    {
        onQuestStarted?.Invoke(quest);

        Debug.Log("Invoking on queststarted " + quest.title);
    }
    public static void OnQuestCompleted(Quest quest)
    {
        onQuestCompleted?.Invoke(quest);
    }
    public static void OnGoalStarted(Goal goal)
    {
        onGoalStarted?.Invoke(goal);
    }
    public static void OnGoalCompleted(Goal goal)
    {
        onGoalCompleted?.Invoke(goal);
    }
    public static void OnPointGained(GoalType type, int id)
    {
        onPointGained?.Invoke(type, id);
    }

    public static void StartQuestline(Questline questlineToStart)
    {
        Questline copiedQuestline = DeepCopyQuestline(questlineToStart);

        SaveData.activeQuestLines.Add(copiedQuestline);
        copiedQuestline.StartQuestline();
    }

    static Questline DeepCopyQuestline(Questline questlineToCopy)
    {
        Questline questlineCopy = new Questline(questlineToCopy.title, new List<Quest>());
        questlineCopy.questToStartAt = questlineToCopy.questToStartAt;
        foreach (Quest quest in questlineToCopy.activeQuests)
        {
            Quest copiedQuest = new Quest(quest.title, quest.questStyle, new List<Goal>());
            foreach (Goal goal in quest.activeGoals)
            {
                copiedQuest.activeGoals.Add(new Goal(goal.goalDescription, goal.goalValue, goal.goalType, goal.itemId));
            }

            questlineCopy.activeQuests.Add(copiedQuest);
        }
        return questlineCopy;
    }

    #region open quest window

    #endregion
}

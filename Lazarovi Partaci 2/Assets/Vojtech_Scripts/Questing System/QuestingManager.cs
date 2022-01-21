using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class QuestingManager
{
    public static Action<Quest> onQuestAccept;
    public static Action<Quest> onQuestComplete;
    public static Action<Goal> onGoalComplete;
    public static Action<GoalType, int> onPointGain;

    public static void OnQuestAccept(Quest quest)
    {
        onQuestAccept?.Invoke(quest);
    }
    public static void OnQuestComplete(Quest quest)
    {
        onQuestComplete?.Invoke(quest);
    }
    public static void OnGoalComplete(Goal goal)
    {
        onGoalComplete?.Invoke(goal);
    }
    public static void OnPointGain(GoalType type, int id)
    {
        onPointGain?.Invoke(type, id);
    }

    public static void StartQuestline(Questline questlineToStart)
    {
        Questline copiedQuestline = DeepCopyQuestline(questlineToStart);

        SaveData.activeQuestLines.Add(copiedQuestline);
        copiedQuestline.StartQuestline();
    }

    static Questline DeepCopyQuestline(Questline questlineToCopy)
    {
        Questline questlineCopy = new Questline(questlineToCopy.title, new List<Quest>(), questlineToCopy.available);
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

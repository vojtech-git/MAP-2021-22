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

    public Text uiQuestTitle;

    public bool IsComplete()
    {
        foreach (QuestGoal questGoal in activeQuestGoals)
        {
            if (!questGoal.IsComplete())
                return false;
        }
        return true;
    }
}

public enum QuestStyle
{
    AfterEachOther,
    AtTheSameTime
}
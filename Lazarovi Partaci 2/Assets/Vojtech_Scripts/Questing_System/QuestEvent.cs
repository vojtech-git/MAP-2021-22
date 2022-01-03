using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestEvent : MonoBehaviour
{
    public List<QuestEventInfo> info;

    protected virtual void Start()
    {
        QuestingSystem.onQuestAccept += OnQuestAccept;
        QuestingSystem.onQuestComplete += OnQuestComplete;
        QuestingSystem.onGoalComplete += OnGoalComplete;
    }

    protected abstract void OnQuestAccept(string title);

    protected abstract void OnQuestComplete(string title);

    protected abstract void OnGoalComplete(string title);

    protected virtual void OnDestroy()
    {
        QuestingSystem.onQuestAccept -= OnQuestAccept;
        QuestingSystem.onQuestComplete -= OnQuestComplete;
        QuestingSystem.onGoalComplete -= OnGoalComplete;
    }
}
public enum AtWhatStage
{
    QuestStart, GoalEnd, QuestEnd
}

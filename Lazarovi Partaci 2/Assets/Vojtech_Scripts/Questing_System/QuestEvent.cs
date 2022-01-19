using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestEvent : MonoBehaviour
{
    public string forWhatQuest;
    public AtWhatStage atWhatStage;

    protected virtual void Start()
    {
        QuestingManager.onQuestAccept += OnQuestAccept;
        QuestingManager.onQuestComplete += OnQuestComplete;
        QuestingManager.onGoalComplete += OnGoalComplete;
    }

    protected abstract void OnQuestAccept(string title);

    protected abstract void OnQuestComplete(string title);

    protected abstract void OnGoalComplete(string title);

    protected virtual void OnDestroy()
    {
        QuestingManager.onQuestAccept -= OnQuestAccept;
        QuestingManager.onQuestComplete -= OnQuestComplete;
        QuestingManager.onGoalComplete -= OnGoalComplete;
    }
}
public enum AtWhatStage
{
    QuestStart, GoalEnd, QuestEnd
}

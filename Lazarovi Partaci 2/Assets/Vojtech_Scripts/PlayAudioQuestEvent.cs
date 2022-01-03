using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioQuestEvent : MonoBehaviour
{
    public PlayAudioEventInfo[] eventInfo;

    void Start()
    {
        QuestingSystem.onQuestAccept += OnQuestAccept;
        QuestingSystem.onQuestComplete += OnQuestComplete;
        QuestingSystem.onGoalComplete += OnGoalComplete;
    }

    void OnQuestAccept(string title)
    {

        foreach (PlayAudioEventInfo infoinstance in eventInfo)
        {
            if (title == infoinstance.forWhatQuest && infoinstance.atWhatStage == AtWhatStage.QuestStart)
            {
                GameStateManager.Instance.PlayAudioMethod(infoinstance.audioSources, infoinstance.talkQuestID);
            }
        }
    }

    void OnQuestComplete(string title)
    {
        foreach (PlayAudioEventInfo infoinstance in eventInfo)
        {
            if (title == infoinstance.forWhatQuest && infoinstance.atWhatStage == AtWhatStage.QuestEnd)
            {
                GameStateManager.Instance.PlayAudioMethod(infoinstance.audioSources, infoinstance.talkQuestID);
            }
        }
    }

    void OnGoalComplete(string title)
    {
        foreach (PlayAudioEventInfo infoinstance in eventInfo)
        {
            if (title == infoinstance.forWhatQuest && infoinstance.atWhatStage == AtWhatStage.GoalEnd)
            {
                GameStateManager.Instance.PlayAudioMethod(infoinstance.audioSources, infoinstance.talkQuestID);
            }
        }
    }

    void OnDestroy()
    {
        QuestingSystem.onQuestAccept -= OnQuestAccept;
        QuestingSystem.onQuestComplete -= OnQuestComplete;
        QuestingSystem.onGoalComplete -= OnGoalComplete;
    }
}

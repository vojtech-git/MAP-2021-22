using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPlayAudio : MonoBehaviour
{
    public string forWhatQuest;
    public AtWhatStage atWhatStage;
    public AudioSource[] audioSources;

    void Start()
    {
        QuestingManager.onQuestAccept += OnQuestAccept;
        QuestingManager.onQuestComplete += OnQuestComplete;
        QuestingManager.onGoalComplete += OnGoalComplete;
    }

    void OnQuestAccept(string title)
    {
        if (title == forWhatQuest && atWhatStage == AtWhatStage.QuestStart)
        {
            GameStateManager.Instance.PlayAudioMethod(audioSources);

            Debug.Log("OnQuestAccept probehlo");
        }
    }

    void OnQuestComplete(string title)
    {
        if (title == forWhatQuest && atWhatStage == AtWhatStage.QuestEnd)
        {
            GameStateManager.Instance.PlayAudioMethod(audioSources);
        }
    }

    void OnGoalComplete(string title)
    {
        if (title == forWhatQuest && atWhatStage == AtWhatStage.GoalEnd)
        {
            GameStateManager.Instance.PlayAudioMethod(audioSources);
        }
    }

    void OnDestroy()
    {
        QuestingManager.onQuestAccept -= OnQuestAccept;
        QuestingManager.onQuestComplete -= OnQuestComplete;
        QuestingManager.onGoalComplete -= OnGoalComplete;
    }
}

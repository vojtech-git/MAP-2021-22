using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPlayAudio : MonoBehaviour
{
    public string whichQuest;
    public bool atTheEnd;
    public AudioSource[] audioSources;

    private void Awake()
    {
        QuestingManager.onQuestStarted += OnQuestStarted;
        QuestingManager.onQuestCompleted += OnQuestCompleted;

        Debug.Log("Adding audio listener " + gameObject.name);
    }

    public void OnQuestStarted(Quest questSender)
    {
        if (questSender.title == whichQuest && !atTheEnd)
        {
            SceneStateManager.Instance.PlayAudioMethod(audioSources);
        }

    }
    public void OnQuestCompleted(Quest questSender)
    {
        if (questSender.title == whichQuest && atTheEnd)
        {
            SceneStateManager.Instance.PlayAudioMethod(audioSources);
        }
    }
}

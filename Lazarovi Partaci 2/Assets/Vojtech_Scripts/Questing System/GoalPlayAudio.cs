using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlayAudio : MonoBehaviour
{
    public string whichGoal;
    public bool atTheEnd;
    public AudioSource[] audioSources;

    private void Awake()
    {
        QuestingManager.onGoalStarted += OnGoalStarted;
        QuestingManager.onGoalCompleted += OnGoalCompleted;

        Debug.Log("Adding audio listener " + gameObject.name);
    }

    public void OnGoalStarted(Goal goalSender)
    {
        if (goalSender.goalDescription == whichGoal && !atTheEnd)
        {
            SceneStateManager.Instance.PlayAudioMethod(audioSources);
        }

    }
    public void OnGoalCompleted(Goal goalSender)
    {
        if (goalSender.goalDescription == whichGoal && atTheEnd)
        {
            SceneStateManager.Instance.PlayAudioMethod(audioSources);
        }
    }
}

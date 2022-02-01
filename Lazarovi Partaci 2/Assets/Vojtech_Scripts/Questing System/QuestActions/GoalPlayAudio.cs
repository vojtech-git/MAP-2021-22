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

        //Debug.Log("Adding audio listener " + gameObject.name);
    }
    private void OnDestroy()
    {
        QuestingManager.onGoalStarted -= OnGoalStarted;
        QuestingManager.onGoalCompleted -= OnGoalCompleted;
    }

    public void OnGoalStarted(Goal goalSender)
    {
        if (goalSender.goalDescription == whichGoal && !atTheEnd)
        {
            StartCoroutine(PlayAudio());
        }

    }
    public void OnGoalCompleted(Goal goalSender)
    {
        if (goalSender.goalDescription == whichGoal && atTheEnd)
        {
            StartCoroutine(PlayAudio());
        }
    }

    public virtual IEnumerator PlayAudio()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] != null)
            {
                if (audioSources[i].isPlaying)
                {
                    audioSources[i].Stop();
                }

                audioSources[i].Play();
                yield return new WaitUntil(() => !audioSources[i].isPlaying); // wait untill musi pøijmout func jako parametr. proto vytvaøím anonym metodu
            }
        }
    }
}

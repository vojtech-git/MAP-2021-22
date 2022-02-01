using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalStartHyperdrive : MonoBehaviour
{
    public string whichGoal;
    public bool atTheEnd;

    private void Awake()
    {
        QuestingManager.onGoalStarted += OnGoalStarted;
        QuestingManager.onGoalCompleted += OnGoalCompleted;

        //Debug.Log("adding hyperdrive listener " + gameObject.name);
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
            Debug.Log("Hyperdrive supposed to start");
        }

    }
    public void OnGoalCompleted(Goal goalSender)
    {
        if (goalSender.goalDescription == whichGoal && atTheEnd)
        {
            Debug.Log("Hyperdrive supposed to start");
        }
    }
}

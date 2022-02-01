using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSendFollowerToPoint : MonoBehaviour
{
    public string whichGoal;
    public bool atTheEnd;
    public FollowerEntity[] followers;

    private void Awake()
    {
        QuestingManager.onGoalStarted += OnGoalStarted;
        QuestingManager.onGoalCompleted += OnGoalCompleted;

        //Debug.Log("Adding listener change obj state " + gameObject.name);
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
            foreach (FollowerEntity item in followers)
            {
                item.shouldGoto = true;
            }
        }
    }
    public void OnGoalCompleted(Goal goalSender)
    {
        if (goalSender.goalDescription == whichGoal && atTheEnd)
        {
            foreach (FollowerEntity item in followers)
            {
                item.shouldGoto = true;
            }
        }
    }
}

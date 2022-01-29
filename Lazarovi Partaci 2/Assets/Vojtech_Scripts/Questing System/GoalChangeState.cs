using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChangeState : MonoBehaviour
{
    public string whichGoal;
    public bool atTheEnd;
    public ChageStateStructure[] objects;

    private void Awake()
    {
        QuestingManager.onGoalStarted += OnGoalStarted;
        QuestingManager.onGoalCompleted += OnGoalCompleted;

        Debug.Log("Adding listener change obj state " + gameObject.name);
    }

    public void OnGoalStarted(Goal goalSender)
    {
        if (goalSender.goalDescription == whichGoal && !atTheEnd)
        {
            foreach (ChageStateStructure item in objects)
            {
                if (item.objectToChange != null)
                {
                    item.objectToChange.SetActive(item.targetState);
                }
            }
        }
    }
    public void OnGoalCompleted(Goal goalSender)
    {
        if (goalSender.goalDescription == whichGoal && atTheEnd)
        {
            foreach (ChageStateStructure item in objects)
            {
                if (item.objectToChange != null)
                {
                    item.objectToChange.SetActive(item.targetState);
                }
            }
        }
    }
}

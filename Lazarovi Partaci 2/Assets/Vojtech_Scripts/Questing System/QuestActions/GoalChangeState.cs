using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChangeState : MonoBehaviour
{
    public string whichGoal;
    public bool atTheEnd;
    public ChangeStateStructure[] objects;

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
            ChangeObjState();
        }
    }
    public void OnGoalCompleted(Goal goalSender)
    {
        if (goalSender.goalDescription == whichGoal && atTheEnd)
        {
            ChangeObjState();
        }
    }

    public virtual void ChangeObjState()
    {
        foreach (ChangeStateStructure item in objects)
        {
            if (item.objectToChange != null)
            {
                item.objectToChange.SetActive(item.targetState);
            }
        }
    }
}

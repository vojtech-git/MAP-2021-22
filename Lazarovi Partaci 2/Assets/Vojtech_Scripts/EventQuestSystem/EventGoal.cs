using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EventGoal
{
    public string goalDescription;
    [Header("Goal Specifikace")]
    public int goalValue;
    public int itemId;
    public GoalType goalType;

    Text uiDescription;

    int currentValue;
    public int CurrentValue
    { 
        get 
        {
            return currentValue;
        } 
        set
        {
            currentValue = value;
            Debug.Log(uiDescription);
            uiDescription.text = $"{goalDescription} {currentValue}/{goalValue}";
            EventQuestingManager.OnGoalProgressed(this);
            if (currentValue >= goalValue)
            {
                CompleteGoal();
            }
        }
    }
    bool complete;
    public bool Complete { get { return currentValue >= goalValue;  } private set { complete = value; } }

    public void StartGoal(Transform questUIParent)
    {
        EventQuestingManager.OnGoalStarted(this);

        uiDescription = GameObject.Instantiate(EventQuestingManager.Instance.goalDescriptionPrefab, questUIParent).GetComponent<Text>();
        Debug.Log(uiDescription);
        CurrentValue = 0;
    }
    public void CompleteGoal()
    {
        EventQuestingManager.OnGoalCompleted(this);

        GameObject.Destroy(uiDescription.gameObject);
    }
}

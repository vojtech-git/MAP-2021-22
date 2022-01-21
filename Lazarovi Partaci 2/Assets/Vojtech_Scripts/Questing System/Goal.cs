using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Goal
{
    public string goalDescription;
    public int goalValue;
    public GoalType goalType;
    public int itemId;

    private int currentValue;
    public int CurrentValue 
    { 
        private get { return currentValue; }
        set
        {
            currentValue = value;
            uiDescription.text = $"{goalDescription} {currentValue}/{goalValue}";
            if (Completed)
            {
                CompleteGoal();
            }
        }
    }

    private bool completed;
    public bool Completed 
    { 
        get
        {
            if (currentValue >= goalValue)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
        private set { completed = value; }
    }

    Text uiDescription;

    public Goal (string _goalDescription, int _goalValue, GoalType _goalType, int _itemId)
    {
        goalDescription = _goalDescription;
        goalValue = _goalValue;
        goalType = _goalType;
        itemId = _itemId;
    }

    public void StartGoal(Transform parentOfUI)
    {
        InstantiateUI(parentOfUI);
        CurrentValue = 0;
    }

    public void ProgressGoal()
    {
        CurrentValue++;
    }

    public void CompleteGoal()
    {
        QuestingManager.OnGoalComplete(this);

        GameObject.Destroy(uiDescription.gameObject);
    }

    void InstantiateUI(Transform parent)
    {
        uiDescription = GameObject.Instantiate(GameStateManager.Instance.goalDescriptionPrefab, parent).GetComponent<Text>();
    }
}

public enum GoalType
{
    Reach,
    Gather,
    Kill
}
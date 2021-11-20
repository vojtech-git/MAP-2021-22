using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestGoal
{
    [Header("Zadání questu")]
    public string goalDescription;
    public int goalValue;
    public GoalType goalType;
    public int itemID;

    [Header("Mìnící se, UI")]
    int currentValue;
    public Text uiGoalDescription;

    //tohle snad nebudu potrebovat vyresim pomoci quest number
    //public GameObject[] onGoalCompleteEnable;
    //public GameObject[] onGoalCompleteDisable;

    public void AddPoint()
    {
        currentValue++;
        PutValuesOnUi();
    }

    public bool IsComplete()
    {
        if (currentValue >= goalValue)
        {
            return true;
        }
        return false;
    }
    
    public void PutValuesOnUi()
    {
        uiGoalDescription.text = $"{goalDescription} {currentValue}/{goalValue}";
    }
}

public enum GoalType
{
    Reach,
    Gather,
}
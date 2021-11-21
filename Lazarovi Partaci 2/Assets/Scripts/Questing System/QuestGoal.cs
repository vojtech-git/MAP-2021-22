using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestGoal
{
    [Header("Zad�n� questu")]
    public string goalDescription;
    public int goalValue;
    public GoalType goalType;
    public int itemID;

    [Header("M�n�c� se, UI")]
    int currentValue;
    public Text uiGoalDescription;

    //tohle snad nebudu potrebovat vyresim pomoci quest number
    //public GameObject[] onGoalCompleteEnable;
    //public GameObject[] onGoalCompleteDisable;

    public void Accept()
    {
        // on questStarted EventHandler
    }

    public void AddPoint()
    {
        currentValue++;
        PutValuesOnUi();

        // point added event hadnler? mo�n�?
    }

    public void Complete()
    {
        GameObject.Destroy(uiGoalDescription.gameObject);
        QuestingSystem.uiManager.ShowMessageFor5Sec($"�ast �kolu {goalDescription} byla spln�na", 2);

        //on goalComplete event handler
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
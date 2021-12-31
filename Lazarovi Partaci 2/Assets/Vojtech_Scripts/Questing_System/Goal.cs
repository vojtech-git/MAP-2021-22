using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Goal
{
    [Header("Zadání questu")]
    public string goalDescription;
    public int goalValue;
    public GoalType goalType;
    public int itemID;

    [Header("UI")]
    Text uiDescription;

    int currentValue;

    public void Accept(Transform parentOfUI)
    {
        InstantiateUI(parentOfUI);

        PutValuesOnUi();
    }

    public void AddPoint()
    {
        currentValue++;
        PutValuesOnUi();
    }

    public void Complete()
    {
        GameObject.Destroy(uiDescription.gameObject);
        GameStateManager.Instance.ShowMessageFor5Sec($"èast úkolu {goalDescription} byla splnìna", 2);
        QuestingSystem.OnGoalComplete(goalDescription);
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
        uiDescription.text = $"{goalDescription} {currentValue}/{goalValue}";
    }

    void InstantiateUI(Transform parent)
    {
        uiDescription = GameObject.Instantiate(GameStateManager.Instance.goalDescriptionPrefab, parent).GetComponent<Text>(); //vytvoø ui reprezentaci pro dalsi questGoal
    }
}

public enum GoalType
{
    Reach,
    Gather,
    Kill
}
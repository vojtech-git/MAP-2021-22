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
    public AudioSource[] completeGoalAudio;

    [Header("Mìnící se, UI")]
    int currentValue;
    Text uiGoalDescription;

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
        GameObject.Destroy(uiGoalDescription.gameObject);
        GameStateManager.Instance.ShowMessageFor5Sec($"èast úkolu {goalDescription} byla splnìna", 2); 
        if (completeGoalAudio != null)
            GameStateManager.Instance.StartCoroutine(GameStateManager.Instance.PlayAudio(completeGoalAudio));
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

    void InstantiateUI(Transform parent)
    {
        uiGoalDescription = GameObject.Instantiate(GameStateManager.Instance.goalDescriptionPrefab, parent).GetComponent<Text>(); //vytvoø ui reprezentaci pro dalsi questGoal
    }
}

public enum GoalType
{
    Reach,
    Gather,
    Kill
}
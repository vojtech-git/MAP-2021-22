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
    public AudioSource[] completeGoalAudio;

    [Header("M�n�c� se, UI")]
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
        GameStateManager.Instance.ShowMessageFor5Sec($"�ast �kolu {goalDescription} byla spln�na", 2); 
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
        uiGoalDescription = GameObject.Instantiate(GameStateManager.Instance.goalDescriptionPrefab, parent).GetComponent<Text>(); //vytvo� ui reprezentaci pro dalsi questGoal
    }
}

public enum GoalType
{
    Reach,
    Gather,
    Kill
}
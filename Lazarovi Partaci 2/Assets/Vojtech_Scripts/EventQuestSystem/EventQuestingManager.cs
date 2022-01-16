using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQuestingManager : MonoBehaviour
{
    public EventQuest firstQuest;
    bool startOfGame = true;

    [Header("Quest UI")]
    public GameObject questUiParent;
    [Header("Quest Prefaby")]
    public GameObject questParentPrefab;
    public GameObject questTitlePrefab;
    public GameObject goalDescriptionPrefab;


    public static Action<int, GoalType> onPointGained;

    public static Action<EventQuest> onQuestStarted;
    public static Action<EventQuest> onQuestProgressed;
    public static Action<EventQuest, string> onQuestCompleted;

    public static Action<EventGoal> onGoalStarted;
    public static Action<EventGoal> onGoalProgressed;
    public static Action<EventGoal> onGoalCompleted;

    private static EventQuestingManager instance;
    public static EventQuestingManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        if (startOfGame)
        {
            firstQuest.StartQuest();
            startOfGame = false;
        }
    }

    public static void OnPointGained(int id, GoalType type)
    {
        onPointGained?.Invoke(id, type);
    }

    public static void OnGoalStarted(EventGoal goal)
    {
        onGoalStarted?.Invoke(goal);
    }
    public static void OnGoalProgressed(EventGoal goal)
    {
        onGoalProgressed?.Invoke(goal);
    }
    public static void OnGoalCompleted(EventGoal goal)
    {
        onGoalCompleted?.Invoke(goal);
    }

    public static void OnQuestStarted(EventQuest quest)
    {
        onQuestStarted?.Invoke(quest);
    }
    public static void OnQuestProgressed(EventQuest quest)
    {
        onQuestProgressed?.Invoke(quest);
    }
    public static void OnQuestCompleted(EventQuest quest, string nextQuest)
    {
        onQuestCompleted?.Invoke(quest, nextQuest);
    }
}

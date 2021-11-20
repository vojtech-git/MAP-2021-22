using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestingSystem : MonoBehaviour
{
    public static int QuestingNumber;
    public static Dictionary<string, Quest> activeQuests = new Dictionary<string, Quest>();
    public static Dictionary<string, Quest> playerCompletedQuests = new Dictionary<string, Quest>();

    public static UIManager uiManager; // v kazdy scene bude pod playerem questingUIManager kterej se bude starat o ui v tý scenì, nebude univerzalni jen pro questing :(

    void Start()
    {
        uiManager = Object.FindObjectOfType<UIManager>();
    }

    public static void AcceptQuest(Quest quest)
    {
        activeQuests.Add(quest.title, quest);

        uiManager.ShowMessageFor5Sec($"úkol {quest.title} byl pøijat", 1);

        quest.InstatiateQuestUI();
    }

    public static void ProgressQuests(GoalType _goalType, int _itemID)
    {
        List<Quest> questsToComplete = new List<Quest>();

        foreach (KeyValuePair<string, Quest> quest in activeQuests)
        {
            quest.Value.AddPoint(_goalType, _itemID);
            if (quest.Value.IsComplete())
            {
                GameObject.Destroy(quest.Value.uiQuestTitle.gameObject);
                questsToComplete.Add(quest.Value);
            }
        }

        foreach (Quest quest in questsToComplete)
        {
            QuestCompleted(quest);
        }
    }

    static void QuestCompleted(Quest completedQuest)
    {
        playerCompletedQuests.Add(completedQuest.title, completedQuest);
        activeQuests.Remove(completedQuest.title);
        uiManager.ShowMessageFor5Sec($"úkol {completedQuest.title} byl splnìn", 1);
    }
}

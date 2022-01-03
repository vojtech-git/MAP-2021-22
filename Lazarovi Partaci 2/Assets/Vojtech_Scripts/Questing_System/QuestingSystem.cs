using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class QuestingSystem
{
    public static int progressNumber = 0;

    public static Dictionary<string, Quest> activeQuests = new Dictionary<string, Quest>();
    public static Dictionary<string, Quest> completedQuests = new Dictionary<string, Quest>();

    public static bool questMenuOpen = false;

    public static bool isStartOfGame = true;

    internal static Action<string> onQuestAccept;
    internal static Action<string> onQuestComplete;
    internal static Action<string> onGoalComplete;

    public static void OnQuestAccept(string qTitle)
    {
        onQuestAccept?.Invoke(qTitle);
    }
    public static void OnQuestComplete(string qTitle)
    {
        onQuestComplete?.Invoke(qTitle);
    }
    public static void OnGoalComplete(string qTitle)
    {
        onGoalComplete?.Invoke(qTitle);
    }

    public static void AcceptQuest(Quest quest)
    {
        if (!activeQuests.ContainsKey(quest.title) && !completedQuests.ContainsKey(quest.title))
        {
            activeQuests.Add(quest.title, quest);

            quest.Accept();
        }
    }

    public static void ProgressQuests(GoalType _goalType, int _itemID)
    {
        List<Quest> questsToComplete = new List<Quest>();

        foreach (KeyValuePair<string, Quest> quest in activeQuests)
        {
            quest.Value.AddPoint(_goalType, _itemID);

            if (quest.Value.IsComplete())
            {
                questsToComplete.Add(quest.Value);
            }
        }

        CompleteQuests(questsToComplete);
    }

    static void CompleteQuests(List<Quest> questsToComplete)
    {
        foreach (Quest quest in questsToComplete)
        {
            activeQuests.Remove(quest.title);
            completedQuests.Add(quest.title, quest);
            quest.Complete();
        }
    }

    public static void OpenQuestMenu(List<Quest> questsToDisplay)
    {
        questMenuOpen = true;
        GameStateManager.Instance.questSelectionMenu.SetActive(true);

        // nejak lepe vyresit celkove vypnout ovladani playera
        GameStateManager.Instance.FPS.GetComponentInChildren<Mouse>().enabled = false;
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;

        if (questsToDisplay.Count == 0)
        {
            GameStateManager.Instance.NoQuestAvailable.enabled = true;
            GameStateManager.Instance.QuestAcceptButton.SetActive(false);
            return;
        }

        GameStateManager.Instance.NoQuestAvailable.enabled = false;
        GameStateManager.Instance.QuestAcceptButton.SetActive(true);


        // cashne vìci z ui manageru
        GameObject tabButtonPrefab = GameStateManager.Instance.questTabButtonPrefab;
        HorizontalLayoutGroup tabButtonsLayoutGroup = GameStateManager.Instance.questLayout;
        TabGroup menuTabGroup = tabButtonsLayoutGroup.GetComponent<TabGroup>();

        // pøes interact raycast zavolam metodu která pošle pole questù z questgivera se kterým interaguju
        foreach (Quest quest in questsToDisplay)
        {
            // instanciatnu pro kazdej quest jeho button a dám ho do Quest_Layoutu
            GameObject tabButton = GameObject.Instantiate(tabButtonPrefab, tabButtonsLayoutGroup.gameObject.transform);
            tabButton.GetComponent<UiTabButton>().quest = quest;
        }

        menuTabGroup.OnTabSelected(menuTabGroup.tabButtons[0]);
    }

    public static void CloseQuestMenu()
    {
        questMenuOpen = false;
        GameStateManager.Instance.questSelectionMenu.SetActive(false);
        GameStateManager.Instance.questLayout.GetComponent<TabGroup>().UnsbscribeAll();
        foreach (Transform child in GameStateManager.Instance.questLayout.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // nejak lepe vyresit celkove vypnout ovladani playera
        GameStateManager.Instance.FPS.GetComponentInChildren<Mouse>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

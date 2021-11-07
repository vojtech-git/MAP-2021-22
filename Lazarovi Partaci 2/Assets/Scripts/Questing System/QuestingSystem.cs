using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestingSystem : MonoBehaviour
{
    public static int QuestingNumber;
    public static Dictionary<string, Quest> playerActiveQuests = new Dictionary<string, Quest>();
    public static Dictionary<string, Quest> playerCompletedQuests = new Dictionary<string, Quest>();

    static UIManager uiManager;

    static List<Quest> questsToComplete = new List<Quest>();
    static List<QuestGoal> goalsToComplete = new List<QuestGoal>();

    void Update()
    {

    }

    void Start()
    {
        uiManager = Object.FindObjectOfType<UIManager>();
    }

    public static void AcceptQuest(Quest quest)
    {
        playerActiveQuests.Add(quest.title, quest);

        uiManager.ShowMessageFor5Sec($"úkol {quest.title} byl pøijat", 1);

        #region Instanciace UI elementù
        quest.uiQuestTitle = Instantiate(uiManager.questTitlePrefab, uiManager.questVerticalLayout.transform).GetComponent<Text>();
        quest.uiQuestTitle.GetComponent<Text>().text = quest.title;
        if (quest.questStyle == QuestStyle.AtTheSameTime)
        {
            foreach (QuestGoal questGoal in quest.activeQuestGoals)
            {
                questGoal.uiGoalDescription = GameObject.Instantiate(uiManager.goalDescriptionPrefab, uiManager.questVerticalLayout.transform).GetComponent<Text>();
                questGoal.uiGoalDescription.text = $"{questGoal.goalDescription} {questGoal.currentValue}/{questGoal.goalValue}";
            }
        }
        else if (quest.questStyle == 0) // after each other
        {
            quest.activeQuestGoals[0].uiGoalDescription = GameObject.Instantiate(uiManager.goalDescriptionPrefab, uiManager.questVerticalLayout.transform).GetComponent<Text>();
            quest.activeQuestGoals[0].uiGoalDescription.text = $"{quest.activeQuestGoals[0].goalDescription} {quest.activeQuestGoals[0].currentValue}/{quest.activeQuestGoals[0].goalValue}";
        }

        #endregion
    }

    public static void ProgressQuests(GoalType _goalType, int _itemID)
    {
        foreach (KeyValuePair<string, Quest> questPair in playerActiveQuests) // pro kazdy quest v aktivnich questech
        {
            if (questPair.Value.questStyle == QuestStyle.AfterEachOther) // pokud ma style za sebou
            {
                if (_goalType == questPair.Value.activeQuestGoals[0].goalType && _itemID == questPair.Value.activeQuestGoals[0].itemID) // pro prvni questGoal v øadì questGoalù, pokid se shoduje itemId a questType
                {
                    questPair.Value.activeQuestGoals[0].AddPoint(); // Pøidej bodu do prvniho goalu

                    if (questPair.Value.activeQuestGoals[0].IsComplete()) // pokud je po pøidani pointu completed  
                    {
                        uiManager.ShowMessageFor5Sec($"èast úkolu {questPair.Value.activeQuestGoals[0].goalDescription} splnìna", 2); // debug - mozna tam necham jak budou chtit

                        GameObject.Destroy(questPair.Value.activeQuestGoals[0].uiGoalDescription.gameObject); // vypni jeho ui reprezentaci
                        goalsToComplete.Add(questPair.Value.activeQuestGoals[0]); // pøidej ho do goals to complete

                        if (questPair.Value.activeQuestGoals.Count > 1) // pokud existuje dalsi questGoal
                        {
                            questPair.Value.activeQuestGoals[1].uiGoalDescription = GameObject.Instantiate(uiManager.goalDescriptionPrefab, uiManager.questVerticalLayout.transform).GetComponent<Text>(); //vytvoø ui reprezentaci pro dalsi questGoal
                            questPair.Value.activeQuestGoals[1].uiGoalDescription.text = $"{questPair.Value.activeQuestGoals[1].goalDescription} {questPair.Value.activeQuestGoals[1].currentValue}/{questPair.Value.activeQuestGoals[1].goalValue}";
                        }                        
                    }
                }
            }
            else if (questPair.Value.questStyle == QuestStyle.AtTheSameTime) // pokud ma styl najednou
            {
                foreach (QuestGoal goal in questPair.Value.activeQuestGoals)
                {
                    if (_goalType == goal.goalType && _itemID == goal.itemID)
                    {
                        goal.AddPoint();

                        if (goal.IsComplete())
                        {
                            uiManager.ShowMessageFor5Sec($"èast úkolu {goal.goalDescription} byla splnìna", 2);
                            GameObject.Destroy(goal.uiGoalDescription.gameObject);
                            goalsToComplete.Add(goal);
                        }
                    }
                }
            }

            foreach (QuestGoal goal in goalsToComplete)
            {
                questPair.Value.activeQuestGoals.Remove(goal);
            }
            goalsToComplete.Clear();

            if (questPair.Value.IsComplete())
            {
                GameObject.Destroy(questPair.Value.uiQuestTitle.gameObject);
                questsToComplete.Add(questPair.Value);
            }
        }

        foreach (Quest quest in questsToComplete)
        {
            QuestCompleted(quest);
        }
        questsToComplete.Clear();
    }

    static void QuestCompleted(Quest completedQuest)
    {
        playerCompletedQuests.Add(completedQuest.title, completedQuest);
        playerActiveQuests.Remove(completedQuest.title);
        uiManager.ShowMessageFor5Sec($"úkol {completedQuest.title} byl splnìn", 1);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestingSystem : MonoBehaviour
{
    [SerializeField] public Quest firstQuest;
    bool startOfGame = false;

    public Dictionary<string, Quest> activeQuests = new Dictionary<string, Quest>();
    public Dictionary<string, Quest> completedQuests = new Dictionary<string, Quest>();

    public bool questMenuOpen = false;

    private static QuestingSystem _instance;
    public static QuestingSystem Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        // naèti hodnoty ze saveloadu
    }

    void Start()
    {
        if (startOfGame)
        {
            AcceptQuest(firstQuest);
        }
    }

    public void AcceptQuest(Quest quest)
    {
        if (!activeQuests.ContainsKey(quest.title) && !completedQuests.ContainsKey(quest.title))
        {
            activeQuests.Add(quest.title, quest);

            quest.Accept();
        }
    }

    public void ProgressQuests(GoalType _goalType, int _itemID)
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

    void CompleteQuests(List<Quest> questsToComplete)
    {
        foreach (Quest quest in questsToComplete)
        {
            activeQuests.Remove(quest.title);
            completedQuests.Add(quest.title, quest);
            quest.Complete();
        }
    }

    public void OpenQuestMenu(List<Quest> questsToDisplay)
    {
        questMenuOpen = true;

        if (questsToDisplay.Count == 0)
        {
            GameStateManager.Instance.ShowMessageFor5Sec("Žádné questy na pøijetí", 1);
        }

        GameStateManager.Instance.FPS.GetComponentInChildren<Mouse>().enabled = false;

        GameStateManager.Instance.questSelectionMenu.SetActive(true); //zapne questing okno

        GameObject questTab = GameStateManager.Instance.questTabButtonPrefab; // cashne vìci z ui manageru
        HorizontalLayoutGroup questLayout = GameStateManager.Instance.questLayout;
        TabGroup questTabGroup = questLayout.GetComponent<TabGroup>();

        GameObject layoutDescription = GameStateManager.Instance.descriptionsLayoutPrefab;
        GameObject questTitlePrefab = GameStateManager.Instance.questTitlePrefab;
        GameObject gDescriptionPrefab = GameStateManager.Instance.goalDescriptionPrefab;
        Transform qDescriptionTransfrom = GameStateManager.Instance.qDescriptionLayout.transform;

        GameObject layoutInProgress; // místo na uložení celkovýho quest popis layout do kteryho dávám quest title a goal description

        int index = 0; // index pro foreach
        foreach (Quest quest in questsToDisplay) // pøes interact raycast zavolam metodu která pošle pole questù z questgivera se kterým interaguju
        {
            if (!activeQuests.ContainsKey(quest.title) && !completedQuests.ContainsKey(quest.title))
            {
                GameObject createdButtonGO = Instantiate(questTab, questLayout.gameObject.transform); //instantiate button na vybirani questu

                createdButtonGO.GetComponent<Text>().text = quest.title;

                UiTabButton createdButton = createdButtonGO.GetComponent<UiTabButton>();
                createdButton.quest = quest; // do toho buttonu hodi quest se kterým pracuje

                layoutInProgress = Instantiate(layoutDescription, qDescriptionTransfrom); //celkovej layout quest popisu
                questTabGroup.objectsToSwap.Add(layoutInProgress); // pøidam ho do objektù pro vypnutí/zapnutí na stejnej index jako je øadový èíslo jeho buttonu

                Instantiate(questTitlePrefab, layoutInProgress.transform).GetComponent<Text>().text = quest.title; // jeden quest title prefab instanciate
                foreach (QuestGoal questGoal in quest.activeQuestGoals)
                {
                    Instantiate(gDescriptionPrefab, layoutInProgress.transform).GetComponent<Text>().text = questGoal.goalDescription; // pro kazdej qGoal instance jeho popisu
                }
                Instantiate(GameStateManager.Instance.acceptButtonPrefab, layoutInProgress.transform).GetComponent<AcceptQuestButton>().relatedQuest = quest; // udelam pro kazdej quest 1 accButon a pridam do nej ten quest

                layoutInProgress.SetActive(false);

                //if (index != 0)
                //{
                //    layoutInProgress.SetActive(false); // vypnout celkovej qLayout aby nebyl zadnej zaplej a zapnul se jenom kdyz zmacku button (mohl bych zapnout první) takhle
                //}

                index++; 
            }
        }
    }

    public void CloseQuestMenu()
    {
        questMenuOpen = false;

        GameStateManager.Instance.FPS.GetComponentInChildren<Mouse>().enabled = true;

        GameStateManager.Instance.questLayout.GetComponent<TabGroup>().UnsbscribeAll();
        GameStateManager.Instance.questLayout.GetComponent<TabGroup>().objectsToSwap.Clear();
        GameStateManager.Instance.questSelectionMenu.SetActive(false);
        foreach (Transform child in GameStateManager.Instance.questLayout.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in GameStateManager.Instance.qDescriptionLayout.transform)
        {
            Destroy(child.gameObject);
        }
    }
}

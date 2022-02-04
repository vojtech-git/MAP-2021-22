using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Questline
{
    public string title;
    public List<Quest> activeQuests;
    [HideInInspector]
    public List<Quest> completedQuests = new List<Quest>();

    [Header("debug")]
    // Vlastì zastupuje nulu. Proto ho odèítám pøi zakonèování questlinky. Když questlinka konèí poøád v active questech zbýva tìch posledních [questToStartAt] questù.
    public int questToStartAt;

    public Questline(string _title, List<Quest> _activeQuests)
    {
        title = _title;
        activeQuests = _activeQuests;
    }

    public void StartQuestline()
    {
        QuestingManager.onPointGained += OnPointGained;

        activeQuests[questToStartAt].StartQuest();
    }
    public void CompleteQuestline()
    {
        QuestingManager.onPointGained -= OnPointGained;
        SaveData.activeQuestLines.Remove(this);
        SaveData.completedQuestLines.Add(this);

        Debug.Log("Questline finished " + title);
    }

    public void OnPointGained(GoalType type, int id)
    {
        activeQuests[questToStartAt].ProgressQuest(type, id);

        if (activeQuests[questToStartAt].Completed)
        { 
            MoveToCompletedQuests(activeQuests[questToStartAt]);
                
            if ((activeQuests.Count - questToStartAt) >= 1)
            {
                activeQuests[questToStartAt].StartQuest();
            }
            else
            {
                CompleteQuestline();
            }
        }
    }

    void MoveToCompletedQuests(Quest quest)
    {
        activeQuests.Remove(quest);
        completedQuests.Add(quest);
    }
}

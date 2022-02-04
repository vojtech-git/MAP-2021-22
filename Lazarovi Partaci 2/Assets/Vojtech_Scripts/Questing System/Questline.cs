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
    // Vlast� zastupuje nulu. Proto ho od��t�m p�i zakon�ov�n� questlinky. Kdy� questlinka kon�� po��d v active questech zb�va t�ch posledn�ch [questToStartAt] quest�.
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

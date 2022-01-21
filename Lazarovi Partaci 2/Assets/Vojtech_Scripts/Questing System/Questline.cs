using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Questline
{
    public string title;
    public List<Quest> activeQuests;
    public List<Quest> completedQuests = new List<Quest>();
    public bool available;
    public bool accepted;
    public bool completed;

    public Questline(string _title, List<Quest> _activeQuests, bool _available)
    {
        title = _title;
        activeQuests = _activeQuests;
        available = _available;
    }

    public void StartQuestline()
    {
        QuestingManager.onPointGain += OnPointGained;

        activeQuests[0].StartQuest();
        accepted = true;
    }
    public void CompleteQuestline()
    {
        QuestingManager.onPointGain -= OnPointGained;
        completed = true;
        SaveData.activeQuestLines.Remove(this);
        SaveData.completedQuestLines.Add(this);

        Debug.Log("Questline finished " + title);
    }

    public void OnPointGained(GoalType type, int id)
    {
        activeQuests[0].ProgressQuest(type, id);

        if (activeQuests[0].Completed)
        {
            MoveToCompletedQuests(activeQuests[0]);
            if (activeQuests.Count >= 1)
            {
                activeQuests[0].StartQuest();
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

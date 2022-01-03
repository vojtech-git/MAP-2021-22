using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private List<Quest> qGiverQuests;

    public List<Quest> GetAvailableQuests()
    {
        List<Quest> tempQuests = new List<Quest>();

        foreach (Quest quest in qGiverQuests)
        {
            if (quest.unlocksWhen <= QuestingSystem.progressNumber)
            {
                tempQuests.Add(quest);
            }
        }

        return tempQuests;
    }
}

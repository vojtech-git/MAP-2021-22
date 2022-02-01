using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChangeState : MonoBehaviour
{
    public string whichQuest;
    public bool atTheEnd;
    public ChangeStateStructure[] objects;

    private void Awake()
    {
        QuestingManager.onQuestStarted += OnQuestStarted;
        QuestingManager.onQuestCompleted += OnQuestCompleted;

        Debug.Log("Adding listener change obj state " + gameObject.name);
    }
    private void OnDestroy()
    {
        QuestingManager.onQuestStarted -= OnQuestStarted;
        QuestingManager.onQuestCompleted -= OnQuestCompleted;
    }
    public void OnQuestStarted(Quest questSender)
    {
        if (questSender.title == whichQuest && !atTheEnd)
        {
            foreach (ChangeStateStructure item in objects)
            {
                if (item.objectToChange != null)
                {
                    item.objectToChange.SetActive(item.targetState);
                }
            }
        }
    }
    public void OnQuestCompleted(Quest questSender)
    {
        if (questSender.title == whichQuest && atTheEnd)
        {
            foreach (ChangeStateStructure item in objects)
            {
                if (item.objectToChange != null)
                {
                    item.objectToChange.SetActive(item.targetState);
                }
            }
        }
    }
}

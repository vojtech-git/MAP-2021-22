using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AcceptQuestButton : MonoBehaviour, IPointerClickHandler
{
    public Quest relatedQuest;

    public void OnPointerClick(PointerEventData eventData)
    {
        QuestingSystem.Instance.AcceptQuest(relatedQuest);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AcceptQuestButton : MonoBehaviour, IPointerClickHandler
{
    public Quest relatedQuest;

    public void OnPointerClick(PointerEventData eventData)
    {
        //QuestingManager.AcceptQuest(relatedQuest);
        throw new NotImplementedException();
    }
}

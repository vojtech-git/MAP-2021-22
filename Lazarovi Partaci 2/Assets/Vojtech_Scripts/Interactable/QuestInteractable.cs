using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteractable : Interactable
{
    public int itemId;

    public override void Interact()
    {
        QuestingManager.OnPointGained(GoalType.Interact, itemId);
    }
}

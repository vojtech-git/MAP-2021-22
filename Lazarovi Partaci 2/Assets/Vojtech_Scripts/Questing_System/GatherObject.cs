using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherObject : MonoBehaviour
{
    public int id;

    public void PickupItem()
    {
        QuestingSystem.ProgressQuests(GoalType.Gather, id);

        Destroy(gameObject);
    }
}

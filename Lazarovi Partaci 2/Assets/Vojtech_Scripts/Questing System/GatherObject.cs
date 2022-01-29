using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherObject : MonoBehaviour
{
    public int id;

    public void PickupItem()
    {
        QuestingManager.OnPointGained(GoalType.Gather, id);        

        Destroy(gameObject);
    }
}

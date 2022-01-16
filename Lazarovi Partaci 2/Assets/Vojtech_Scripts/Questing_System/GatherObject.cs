using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherObject : MonoBehaviour
{
    public int id;

    public void PickupItem()
    {
        EventQuestingManager.OnPointGained(id, GoalType.Gather);

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherObject : MonoBehaviour
{
    public int id;

    public void ItemPicked()
    {
        QuestingSystem.Instance.ProgressQuests(GoalType.Gather, id);

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTrigger : MonoBehaviour
{
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        QuestingSystem.ProgressQuests(GoalType.Reach, id);
    }
}

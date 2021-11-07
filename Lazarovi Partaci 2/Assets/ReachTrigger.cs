using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        QuestingSystem.ProgressQuests(GoalType.Reach, 0);
    }
}

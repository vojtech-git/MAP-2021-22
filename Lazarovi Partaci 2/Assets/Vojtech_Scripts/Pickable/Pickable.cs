using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public int itemId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player colliderPlayer = other.GetComponent<Player>();

            Pickup(colliderPlayer);
        }
    }

    /// <summary>
    /// Plays when player enters this gameobjects trigger. Use base to progress quests.
    /// </summary>
    /// <param name="player"></param>
    protected virtual void Pickup(Player player)
    {
        QuestingManager.OnPointGained(GoalType.Gather, itemId);
        Destroy(gameObject);
    }
}

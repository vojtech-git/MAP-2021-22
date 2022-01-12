using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    public int enemyEntityId;
    public GameObject[] ItemsToDropPrefabs;
 
    public void DropLoot()
    {
        int random = Random.Range(0, ItemsToDropPrefabs.Length - 1);
        Debug.Log(random);
        Instantiate(ItemsToDropPrefabs[random], transform.position, Quaternion.identity);
    }

    public override void Die()
    {
        QuestingSystem.ProgressQuests(GoalType.Kill, enemyEntityId);
        DropLoot();
        base.Die();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    public int enemyEntityId;
    public GameObject HealthKitPrefab;
    public GameObject[] MoneyPrefabs;
 
    public override void Die()
    {
        QuestingSystem.Instance.ProgressQuests(GoalType.Kill, enemyEntityId);
        DropLoot();
        base.Die();
    }

    public void DropLoot()
    {
        int random = Random.Range(0, 2);

        Debug.Log(random);

        if (random == 0)
        {
            Instantiate(HealthKitPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(MoneyPrefabs[Random.Range(0, MoneyPrefabs.Length)], transform.position, Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    public int enemyEntityId;

    public override void Die()
    {
        base.Die();
        QuestingSystem.Instance.ProgressQuests(GoalType.Kill, enemyEntityId);
    }
}

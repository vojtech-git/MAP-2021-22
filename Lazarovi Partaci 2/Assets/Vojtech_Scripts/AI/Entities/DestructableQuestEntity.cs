using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableQuestEntity : Entity
{
    [Header("Quest")]
    public int id;

    public override void Die()
    {
        QuestingManager.OnPointGained(GoalType.Kill, id);
        base.Die();
    }
}

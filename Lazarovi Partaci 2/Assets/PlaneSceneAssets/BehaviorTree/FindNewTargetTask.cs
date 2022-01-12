using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNewTargetTask : BTNode
{
    IBehaviorAI myAI;
    string enemyFraction;
    string faction;
    public FindNewTargetTask(IBehaviorAI _myAI, string _enemyFraction)
    {
        myAI = _myAI;
        enemyFraction = _enemyFraction;
    }

    public override BTNodeStates Evaluate()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(enemyFraction);
     

        if(targets.Length > 0)
        {
        int randomChoice = Random.Range(0, targets.Length);
        myAI.SetTarget(targets[randomChoice]);
        return BTNodeStates.SUCCESS;
        }

        else 
        {
            return BTNodeStates.FAILURE;
        }
    }
}

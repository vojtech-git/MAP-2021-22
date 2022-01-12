using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArrivalTask : BTNode
{
    IBehaviorAI myAI;
    public CheckArrivalTask (IBehaviorAI _myAI)
    {
        myAI = _myAI;
    }
      public override BTNodeStates Evaluate()
      {
        Vector3 agentPosition = myAI.GetAgentTransform().position;
        Vector3 targetPosition = myAI.GetTargetPosition();
       float distance = Vector3.Distance(agentPosition, targetPosition);

        if(distance < 100f)
        {
            return BTNodeStates.SUCCESS;
        }
        else 
        {
            return BTNodeStates.FAILURE;
        }
      }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindWanderPointTask : BTNode
{
  float range;
  IBehaviorAI myAI;

  public FindWanderPointTask(IBehaviorAI _myAI, float _range)
  {
      range = _range;
      myAI = _myAI;
  }

    public override BTNodeStates Evaluate()
    {
        myAI.SetTarget(null);
        myAI.SetTargetPosition(Random.insideUnitSphere * range);

        return BTNodeStates.SUCCESS;
    }
}

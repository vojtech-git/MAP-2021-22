using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTargetVisible : BTNode
{
    IBehaviorAI myAI;
    public IsTargetVisible(IBehaviorAI _myAI)
    {   
        myAI = _myAI;
    }
   public override BTNodeStates Evaluate()
   {
       if(myAI.GetTarget() == null) 
       {
           return BTNodeStates.FAILURE;
       }

    RaycastHit hit;
     if(Physics.Raycast(myAI.GetTransform().position, myAI.GetTransform().forward, out hit, 1000f))
    {
        if(hit.collider.transform.root.gameObject == myAI.GetTarget())
        {
            //Debug.Log("TARGET IS VISIBLE");
            return BTNodeStates.SUCCESS;
        }
    }
        else 
        {
            //Debug.Log("TARGET IS NOT VISIBLE");
            return BTNodeStates.FAILURE;
        }

        return BTNodeStates.FAILURE;
   }    
}

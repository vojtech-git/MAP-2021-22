using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToTargetTask : BTNode
{

    IBehaviorAI myAI;

    event InputEventVector TurnEvent;
    
    /* EnemyMovement m_someOtherScriptOnAnotherGameObject; */

   /*    public TurnToTargetTask (IBehaviorAI _myAI, EnemyMovement ene)
    {
        myAI = _myAI;
      //m 

        m_someOtherScriptOnAnotherGameObject = ene;
    } */


        public TurnToTargetTask (IBehaviorAI _myAI, InputEventVector _turnEvent)
    {
        myAI = _myAI;
        TurnEvent = _turnEvent;
      //m 

       /*  m_someOtherScriptOnAnotherGameObject = ene; */
    }
    public override BTNodeStates Evaluate() 
    {
        Vector3 agentPosition = myAI.GetAgentTransform().position;
        Vector3 targetPosition = myAI.GetTargetPosition();

        Vector3 desiredHeading = (targetPosition - agentPosition);
        if(Vector3.Angle(agentPosition, desiredHeading) > 10) 
        {
        if (TurnEvent != null){
       TurnEvent(desiredHeading.x,desiredHeading.y,desiredHeading.z);
        }
        if(TurnEvent == null){
            Debug.Log("JSI V PICI KAMO");
        }
        }

        return BTNodeStates.SUCCESS;

    }
}

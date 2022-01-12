using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetTask : BTNode
{

    IBehaviorAI myAI;  //TADY CHYBI INPUT SYSTEM
    Transform agentPosition;
    Vector3 targetPosition;
    float range;
    /* EnemyMovement m_someOtherScriptOnAnotherGameObject; */
    event InputEventFloat ForwardEvent;
   

    public MoveToTargetTask (IBehaviorAI _myAI, float _range, InputEventFloat _forwardEvent)
    {
        myAI = _myAI;
        range = _range;
        ForwardEvent = _forwardEvent;
    }
      public override BTNodeStates Evaluate()
      {
          Vector3 agentPosition = myAI.GetAgentTransform().position;
          targetPosition = myAI.GetTargetPosition();

          float distance = Vector3.Distance(agentPosition, targetPosition);
          float thrust = distance / range;
          if(ForwardEvent != null) {
              ForwardEvent(thrust);
             /*  m_someOtherScriptOnAnotherGameObject =  GameObject.FindObjectOfType(typeof(EnemyMovement)) as EnemyMovement;
              m_someOtherScriptOnAnotherGameObject.ForwardThrust(thrust); */
          }

          if ( ForwardEvent == null){
              Debug.Log("PRITELI JE TO V HAJZLU");
          }
          return BTNodeStates.SUCCESS;
      }

    void Start()
    {
   /*   m_someOtherScriptOnAnotherGameObject = GameObject.FindObjectOfType(typeof(EnemyMovement)) as EnemyMovement;
    m_someOtherScriptOnAnotherGameObject.ForwardThrust(110); */
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponTask : BTNode
{
    IBehaviorAI myAI;
    InputEvent fireWeaponEvent;
    public FireWeaponTask(IBehaviorAI _myAI, InputEvent _fireWeaponEvent)
    {
        myAI = _myAI;
        fireWeaponEvent = _fireWeaponEvent;
    }
    public override BTNodeStates Evaluate()
    {
       if(fireWeaponEvent != null) 
       {
           fireWeaponEvent();
           //Debug.Log("FIRE WEAPON BYL USPESNE SPUSTEN");
           return BTNodeStates.SUCCESS;
          
       }

       else 
       {
           return BTNodeStates.FAILURE;
       }
    }
}

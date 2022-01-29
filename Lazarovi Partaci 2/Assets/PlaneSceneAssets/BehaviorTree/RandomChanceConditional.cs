using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChanceConditional : BTNode
{
    int numberOfDice;
    int numberOfSides;
    int numberToBeat;
    /* float rangeOfAttack;
     IBehaviorAI myAI;
    GameObject Player; */

    public RandomChanceConditional(int _numberOfDice, int _numberOfSides, int _numberToBeat)
    {
        numberOfDice = _numberOfDice;
        numberOfSides = _numberOfSides;
        numberToBeat = _numberToBeat;
    }
    /*  public RandomChanceConditional(IBehaviorAI _myAI,float _rangeOfAttack, GameObject _Player)
    {
        myAI = _myAI;
        Player = _Player;
        rangeOfAttack = _rangeOfAttack;
    } */
    public override BTNodeStates Evaluate()
    {
        int total = 0;
        for (int i = 0; i < numberOfDice; i++)
        {
            total += Random.Range(1,(numberOfSides+1));
        }
        //Debug.Log(total);

        if (total > numberToBeat)
        {
            return BTNodeStates.SUCCESS;
        }
        else 
        {
            return BTNodeStates.FAILURE;
        }
    }
   /*  public override BTNodeStates Evaluate()
    {
        Vector3 agentPosition = myAI.GetAgentTransform().position;  //Funguje, ale pouze se otoci na hrace
       if (Vector3.Distance(Player.transform.position, agentPosition) < rangeOfAttack)
       {    Debug.Log(Vector3.Distance(Player.transform.position, agentPosition));
            return BTNodeStates.SUCCESS;
       }


        else 
        {
            return BTNodeStates.FAILURE;
        }
    } */
}

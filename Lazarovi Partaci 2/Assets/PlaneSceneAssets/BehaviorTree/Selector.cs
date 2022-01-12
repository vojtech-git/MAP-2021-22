using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : BTNode
{
    protected List<BTNode> myNodes = new List<BTNode>();

    public Selector(List<BTNode> nodes)
    {
        myNodes = nodes;
    }

    public override BTNodeStates Evaluate()
    {
         foreach(BTNode node  in myNodes)
        {
            switch (node.Evaluate())
            {
                case BTNodeStates.FAILURE:
                    continue;

                case BTNodeStates.SUCCESS:
                   currentNodeState = BTNodeStates.SUCCESS;
                   return currentNodeState;

           
                default: 
                    continue;
            }
        }
        currentNodeState = BTNodeStates.FAILURE;
        return currentNodeState;
    }
}

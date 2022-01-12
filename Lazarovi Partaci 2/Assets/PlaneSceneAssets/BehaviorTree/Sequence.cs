using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BTNode 
{
    private List<BTNode> myNodes = new List<BTNode>();   //konstrukce sekvence
    public Sequence(List<BTNode> nodes)
    {
        myNodes = nodes;
    }
    public override BTNodeStates Evaluate()
    {
        bool childRunning = false;

        foreach(BTNode node  in myNodes)
        {
            switch (node.Evaluate())
            {
                case BTNodeStates.FAILURE:
                    currentNodeState = BTNodeStates.FAILURE;
                    return currentNodeState;

                case BTNodeStates.SUCCESS:
                    continue;

                case BTNodeStates.RUNNING:
                    childRunning=true;
                    continue;

                default: 
                    currentNodeState = BTNodeStates.SUCCESS;
                    return currentNodeState;
            }
        }
        currentNodeState  = childRunning ? BTNodeStates.RUNNING : BTNodeStates.SUCCESS;
        return currentNodeState;
    }
}

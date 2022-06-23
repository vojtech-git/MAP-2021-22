using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GoalAction
{
    public GoalActionType actionType;

    [Header("Action:")]

    public float delayLength;
    public AudioSource[] audioToPlay;
    public ChangeStateActionData changeStateData;
    public InstanciateObjectsActionData instanciateObjectsData;
    public ChangeScriptStateActionData changeScriptStateData;
    public ScriptedSeqenceActionData followerStartScriptedSequences;
    public PhoneActionData phoneActionStructure;
}

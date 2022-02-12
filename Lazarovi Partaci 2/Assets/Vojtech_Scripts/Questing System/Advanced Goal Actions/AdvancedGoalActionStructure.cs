using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class AdvancedGoalActionStructure
{
    public GoalActionType actionType;
    [Header("objects to affect")]
    public AudioSource[] audioToPlay;
    public ChangeStateStructure[] changeStateStructures;
    public InstanciateObjectsSructure[] instanciateObjectsSructures;
    public ChangeScriptStateStructure[] changeScriptStateStructures;
    public ScriptedSequenceStructure[] followerStartScriptedSequences;
    public PlayableDirector timelineToStart;
    public float delayLength;
}

public enum GoalActionType
{
    PlayAudio, ChangeObjState, StartTimeline, PlayFollowerScriptedSequence, DelayNextAction, InstanciateObjects, ChangeScriptState
}
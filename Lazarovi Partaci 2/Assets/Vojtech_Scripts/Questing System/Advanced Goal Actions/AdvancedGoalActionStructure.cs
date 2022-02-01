using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class AdvancedGoalActionStructure
{
    public GoalActionType actionType;
    public ChangeStateStructure[] changeStateStructures;
    public AudioSource[] audioToPlay;
    public FollowerEntity followerToSend;
    public PlayableDirector timelineToStart;
    public float delayLength;
}

public enum GoalActionType
{
    PlayAudio, ChangeObjState, StartTimeline, SendFollowerToPoint, DelayNextAction
}
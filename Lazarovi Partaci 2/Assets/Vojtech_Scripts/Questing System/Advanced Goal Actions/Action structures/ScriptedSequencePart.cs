using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptedSequencePart
{
    public SequencePartType sequencePartType;
    [Header("info")]
    public float delay;
    public Vector3 position;
    public Quaternion rotation;
}

public enum SequencePartType
{
    Delay, Move, Rotate, Suicide, ReturnToFollowerState
}

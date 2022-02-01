using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlayAudioStartHyperdrive : GoalPlayAudio
{
    public override IEnumerator PlayAudio()
    {
        yield return base.PlayAudio();

        Debug.Log("supposed to start hyperdrive");
    }
}

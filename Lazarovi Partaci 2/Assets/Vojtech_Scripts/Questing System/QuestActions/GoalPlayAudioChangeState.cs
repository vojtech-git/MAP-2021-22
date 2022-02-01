using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlayAudioChangeState : GoalPlayAudio
{
    public ChangeStateStructure[] objects;

    public override IEnumerator PlayAudio()
    {
        yield return base.PlayAudio();

        foreach (ChangeStateStructure item in objects)
        {
            item.objectToChange.SetActive(item.targetState);
        }
    }
}

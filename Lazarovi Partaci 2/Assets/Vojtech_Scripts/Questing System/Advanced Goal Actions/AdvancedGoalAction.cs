using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AdvancedGoalAction : MonoBehaviour
{
    public bool atTheEnd;
    public string whichGoal;
    public List<AdvancedGoalActionStructure> advancedGoalActionStructures;

    private void Awake()
    {
        QuestingManager.onGoalStarted += OnGoalStarted;
        QuestingManager.onGoalCompleted += OnGoalCompleted;

        //Debug.Log("Adding listener generic goal action on " + gameObject.name);
    }
    private void OnDestroy()
    {
        QuestingManager.onGoalStarted -= OnGoalStarted;
        QuestingManager.onGoalCompleted -= OnGoalCompleted;
    }

    public void OnGoalStarted(Goal goalSender)
    {
        if (goalSender.goalDescription == whichGoal && !atTheEnd)
        {
            StartCoroutine(TriggerAction());
        }
    }
    public void OnGoalCompleted(Goal goalSender)
    {
        if (goalSender.goalDescription == whichGoal && atTheEnd)
        {
            StartCoroutine(TriggerAction());
        }
    }

    public IEnumerator TriggerAction()
    {
        foreach (AdvancedGoalActionStructure structure in advancedGoalActionStructures)
        {
            if (structure.actionType == GoalActionType.PlayAudio)
            {
                for (int i = 0; i < structure.audioToPlay.Length; i++)
                {
                    if (structure.audioToPlay[i] != null)
                    {
                        if (structure.audioToPlay[i].isPlaying)
                        {
                            structure.audioToPlay[i].Stop();
                        }

                        structure.audioToPlay[i].Play();
                        yield return new WaitUntil(() => !structure.audioToPlay[i].isPlaying); // wait untill musi pøijmout func jako parametr. proto vytvaøím anonym metodu
                    }
                }
            }
            else if (structure.actionType == GoalActionType.ChangeObjState)
            {
                foreach (ChangeStateStructure objChangeStructure in structure.changeStateStructures)
                {
                    if (objChangeStructure.objectToChange != null)
                    {
                        objChangeStructure.objectToChange.SetActive(objChangeStructure.targetState);
                    }
                }
            }
            else if (structure.actionType == GoalActionType.StartTimeline)
            {
                Debug.Log("starting hyperdrive");
            }
            else if (structure.actionType == GoalActionType.PlayFollowerScriptedSequence)
            {
                foreach (ScriptedSequenceStructure sequence in structure.followerStartScriptedSequences)
                {
                    sequence.follower.scriptedSequencePlaying = true;

                    foreach (ScriptedSequencePart part in sequence.scriptedSequenceParts)
                    {
                        if (part.sequencePartType == SequencePartType.Delay)
                        {
                            Debug.Log("sequence delaying for " + part.delay);
                            yield return new WaitForSeconds(part.delay);
                            Debug.Log("sequence delayed for " + part.delay);
                        }
                        else if (part.sequencePartType == SequencePartType.Move)
                        {
                            sequence.follower.StartCoroutine(sequence.follower.GoToPosition(part.position));
                            yield return new WaitUntil(() => (sequence.follower.transform.position - part.position).magnitude < sequence.follower.agent.stoppingDistance);

                            Debug.Log("sequence follower at point");
                        }
                        else if (part.sequencePartType == SequencePartType.Rotate)
                        {
                            sequence.follower.gameObject.transform.rotation = part.rotation;
                            Debug.Log("sequence rotated follower");
                        }
                        else if (part.sequencePartType == SequencePartType.Suicide)
                        {
                            sequence.follower.StoryDie();
                            Debug.Log("sequence follower story death");
                        }
                        else if (part.sequencePartType == SequencePartType.ReturnToFollowerState)
                        {
                            sequence.follower.ReturnToFollowerState();
                            Debug.Log("sequence follower returned to followerstate");
                        }
                    }
                }
            }
            else if (structure.actionType == GoalActionType.DelayNextAction)
            {
                yield return new WaitForSeconds(structure.delayLength);
            }
            else if (structure.actionType == GoalActionType.InstanciateObjects)
            {
                foreach (InstanciateObjectsSructure instanciateStructure in structure.instanciateObjectsSructures)
                {
                    foreach (Vector3 objectsPosition in instanciateStructure.positions)
                    {
                        Instantiate(instanciateStructure.objectToInstanciate, objectsPosition, Quaternion.identity);
                    }
                }
            }
            else if (structure.actionType == GoalActionType.ChangeScriptState)
            {
                foreach (ChangeScriptStateStructure scriptChangeStruct in structure.changeScriptStateStructures)
                {
                    scriptChangeStruct.componentToSwitch.enabled = scriptChangeStruct.desiredState;
                }
            }
            else if (structure.actionType == GoalActionType.PhoneCall)
            {
                structure.phoneActionStructure.phone.GetComponent<phoneOnOff>().Volani(structure.phoneActionStructure.whichQuest);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : State
{ 
    private Collider currentTarget;
    private TutorialAI npcAIScript;


    public Chase(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, Collider _target) : base(_npc, _agent, _anim, _player)
    {
        type = StateType.CHASE;
        currentTarget = _target;
    }

    public override void Enter()
    {
        Debug.Log("entered Chase state");

        base.Enter();
        anim.SetBool("isRunning", true);
        agent.isStopped = false;
        agent.speed = 5;
        npcAIScript = npc.GetComponent<TutorialAI>();
    }
    public override void Update()
    {
        base.Update();

        agent.SetDestination(currentTarget.transform.position); // destination se musi setovat v updatu jinak se to neupdatuje

        FindCloserVisibleTarget(); // pokud najde target kterej je blíž než current target tak vyhodí novej chase state s novym targetem

        float distanceToCurrentTarget = Vector3.Distance(npc.transform.position, currentTarget.transform.position);

        if (distanceToCurrentTarget > npcAIScript.looseSightInstantlyDistance)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = StateStage.EXIT;
            return;
        }
        else if (distanceToCurrentTarget > npcAIScript.sightDistance)
        {
            // dodelat logiku chasovani mimo range
            nextState = new Idle(npc, agent, anim, player);
            stage = StateStage.EXIT;
            return;
        }
        else if (distanceToCurrentTarget < npcAIScript.attackDistance)
        {
            nextState = new Attack(npc, agent, anim, player, currentTarget);
            stage = StateStage.EXIT;
            return;
        }
    }

    public override void Exit()
    {
        anim.SetBool("isRunning", false);
        base.Exit();
    }

    void FindCloserVisibleTarget()
    {
        foreach (Collider target in Physics.OverlapSphere(npc.transform.position, npcAIScript.sightDistance, npcAIScript.targetMask))
        {
            float distanceToTarget = Vector3.Distance(npc.transform.position, target.transform.position);

            if (target != currentTarget)
            {
                Vector3 dirToTarget = (target.transform.position - npc.transform.position).normalized;
                if (Vector3.Angle(npc.transform.forward, dirToTarget) < npcAIScript.sightAngle / 2)
                {
                    if (!Physics.Raycast(npc.transform.position, dirToTarget, distanceToTarget, npcAIScript.obstacleMask))
                    {
                        if (distanceToTarget < Vector3.Distance(npc.transform.position, currentTarget.transform.position))
                        {
                            nextState = new Chase(npc, agent, anim, player, target);
                        }
                    }
                } 
            }
        }
    }
}

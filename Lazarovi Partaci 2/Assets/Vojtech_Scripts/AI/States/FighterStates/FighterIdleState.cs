using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FighterIdleState : State
{
    FighterEntity fighterEntity;

    public FighterIdleState(GameObject _npc, NavMeshAgent _agent, Animator _anim, FighterEntity _fighterEntity) : base(_npc, _agent, _anim)
    {
        type = StateType.IDLE;
        fighterEntity = _fighterEntity;
    }

    public override void Enter()
    {
        //Debug.Log(npc.gameObject.name + " enetered shooter idle state");

        fighterEntity.ReturnEntityToPost();

        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // chase kdy�:
        // vid�m target (done)
        // m� n�kdo st�el�
        // target vedle m� vyst�el�

        GameObject target = LookForClosestTarget();

        if (target != null)
        {
            if (fighterEntity.runningToPost != null)
            {
                fighterEntity.StopCoroutine(fighterEntity.runningToPost);
                fighterEntity.runningToPost = null;
            }

            nextState = new FighterChaseState(npc, agent, anim, target, fighterEntity);
            stage = StateStage.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    GameObject LookForClosestTarget()
    {
        Collider currerntTarget = null;
        // v radiusu kolem sebe zjisti kazd�ho potential target
        foreach (Collider target in Physics.OverlapSphere(npc.transform.position, fighterEntity.sightDistance, fighterEntity.targetMask))
        {
            // najdi vzdalenost k targetu a ke currentTargetu
            float distanceToTarget = Vector3.Distance(npc.transform.position, target.transform.position);
            Vector3 dirToTarget = (target.transform.position - npc.transform.position).normalized;

            // pokud je vzd�lenost krat�� ne� attack distance a je bl� ne� currentTarget tak ho setni jako currentTarget
            if (distanceToTarget < fighterEntity.autoDetectDistance && (currerntTarget == null || distanceToTarget < Vector3.Distance(currerntTarget.transform.position, target.transform.position)) && !Physics.Raycast(npc.transform.position, dirToTarget, distanceToTarget, fighterEntity.obstacleMask))
                currerntTarget = target;

            // pokud je target bl� ne� currentTarget
            if (currerntTarget == null || distanceToTarget < Vector3.Distance(npc.transform.position, currerntTarget.transform.position))
            {
                // a je v sightAnglu npcka
                
                if (Vector3.Angle(npc.transform.forward, dirToTarget) < fighterEntity.sightAngle / 2)
                {
                    // a nen� za obstaclem tak ho nastav jako currentTarget
                    if (!Physics.Raycast(npc.transform.position, dirToTarget, distanceToTarget, fighterEntity.obstacleMask))
                    {
                        currerntTarget = target;
                    }
                }
            }
        }

        if (currerntTarget != null)
        {
            return currerntTarget.gameObject;
        }
        else
        {
            return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FighterChaseState : State
{
    FighterEntity fighterEntity;

    GameObject target;

    public FighterChaseState(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _target, FighterEntity _fighterEntity) : base(_npc, _agent, _anim)
    {
        type = StateType.IDLE;
        fighterEntity = _fighterEntity;
        target = _target;
    }

    public override void Enter()
    {
        //Debug.Log(npc.gameObject.name + " enetered shooter chase state");

        agent.isStopped = false;

        anim.SetBool("isRunning", true);
        anim.SetBool("isAttacking", false);

        base.Enter();
    }

    public override void Update()
    {
        if (target != null)
        {
            base.Update();

            GameObject tempTarget = LookForClosestTarget(); // i kdyz chasuje tak hleda jestli nenajde nìjakej bližší target
            if (tempTarget != null && tempTarget != target)
                target = tempTarget;

            agent.SetDestination(target.transform.position);

            float distanceToTarget = Vector3.Distance(npc.transform.position, target.transform.position);

            Vector3 dirToTarget = (target.transform.position - npc.transform.position).normalized;
            if (distanceToTarget < fighterEntity.sightDistance && !Physics.Raycast(npc.transform.position, dirToTarget, distanceToTarget, fighterEntity.obstacleMask))
            {
                if (fighterEntity.chasingTarget != null)
                {
                    fighterEntity.StopCoroutine(fighterEntity.chasingTarget);
                    fighterEntity.chasingTarget = null;
                }
            }

            if (fighterEntity.ReadyToThrowGranade && Vector3.Angle((target.transform.position - npc.transform.position).normalized, npc.transform.forward) < (fighterEntity.sightAngle / 2))
            {
                fighterEntity.ThrowGranade(target.transform);
            }

            if (distanceToTarget < fighterEntity.attackDistance)
            {
                nextState = new FighterAttackState(npc, agent, anim, target, fighterEntity);
                stage = StateStage.EXIT;
            }
            else if (distanceToTarget > fighterEntity.sightDistance)
            {
                if (fighterEntity.chasingTarget == null)
                {
                    fighterEntity.ChaseTargetOutOfRange(() =>
                        {
                            nextState = new FighterIdleState(npc, agent, anim, fighterEntity);
                            stage = StateStage.EXIT;
                        });
                }
            } 
        }
        else
        {
            nextState = new FighterIdleState(npc, agent, anim, fighterEntity);
            stage = StateStage.EXIT;
        }
    }

    GameObject LookForClosestTarget()
    {
        Collider currerntTarget = null;
        // v radiusu kolem sebe zjisti kazdýho potential target
        foreach (Collider target in Physics.OverlapSphere(npc.transform.position, fighterEntity.sightDistance, fighterEntity.targetMask))
        {
            // najdi vzdalenost k targetu a smìr ke currentTargetu
            float distanceToTarget = Vector3.Distance(npc.transform.position, target.transform.position);
            Vector3 dirToTarget = (target.transform.position - npc.transform.position).normalized;

            // pokud je vzdálenost kratší než attack distance a je blíž než currentTarget tak ho setni jako currentTarget
            if (distanceToTarget < fighterEntity.autoDetectRange && (currerntTarget == null || distanceToTarget < Vector3.Distance(currerntTarget.transform.position, target.transform.position)) && !Physics.Raycast(npc.transform.position, dirToTarget, distanceToTarget, fighterEntity.obstacleMask))
                currerntTarget = target;

            // pokud je target blíž než currentTarget
            if (currerntTarget == null || distanceToTarget < Vector3.Distance(npc.transform.position, currerntTarget.transform.position))
            {
                // a je v sightAnglu npcka

                if (Vector3.Angle(npc.transform.forward, dirToTarget) < fighterEntity.sightAngle / 2)
                {
                    // a není za obstaclem tak ho nastav jako currentTarget
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

    public override void Exit()
    {
        base.Exit();
    }
}

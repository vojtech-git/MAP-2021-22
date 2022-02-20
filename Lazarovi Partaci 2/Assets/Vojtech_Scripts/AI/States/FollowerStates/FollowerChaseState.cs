using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerChaseState : State
{
    Transform player;
    FollowerEntity followerEntity;
    GameObject target;

    public FollowerChaseState(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _target, FollowerEntity _followerEntity, Transform _player) : base(_npc, _agent, _anim)
    {
        type = StateType.IDLE;
        followerEntity = _followerEntity;
        target = _target;
        player = _player;
    }

    public override void Enter()
    {
        //Debug.Log(npc.gameObject.name + " enetered follower chase state");

        agent.isStopped = false;
        agent.stoppingDistance = 1;

        anim.SetBool("isRunning", true);
        anim.SetBool("isAttacking", false);

        base.Enter();
    }

    public override void Update()
    {
        if (target != null)
        {
            //Debug.Log(followerEntity.gameObject.name + " " + target.gameObject.name);

        }
        else
        {
            //Debug.Log("no target ");
        }
        if (target != null)
        {
            base.Update();

            GameObject tempTarget = LookForClosestTarget(); // i kdyz chasuje tak hleda jestli nenajde nìjakej bližší target
            if (tempTarget != null && tempTarget != target)
                target = tempTarget;

            agent.SetDestination(target.transform.position);

            float distanceToTarget = Vector3.Distance(npc.transform.position, target.transform.position);

            if (distanceToTarget < followerEntity.sightDistance)
            {
                if (followerEntity.chasingTarget != null)
                {
                    followerEntity.StopCoroutine(followerEntity.chasingTarget);
                    followerEntity.chasingTarget = null;
                }
            }

            if (followerEntity.ReadyToThrowGranade && Vector3.Angle((target.transform.position - npc.transform.position).normalized, npc.transform.forward) < (followerEntity.sightAngle / 2))
            {
                followerEntity.ThrowGranade(target.transform);
            }

            if (distanceToTarget < followerEntity.attackDistance)
            {
                nextState = new FollowerAttackState(npc, agent, anim, target, followerEntity, player);
                stage = StateStage.EXIT;
            }
            else if (distanceToTarget > followerEntity.sightDistance)
            {
                if (followerEntity.chasingTarget == null)
                {
                    followerEntity.ChaseTargetOutOfRange(() =>
                    {
                        nextState = new FollowerIdleState(npc, agent, anim, followerEntity, player);
                        stage = StateStage.EXIT;
                    });
                }
            }
        }
        else
        {
            nextState = new FollowerIdleState(npc, agent, anim, followerEntity, player);
            stage = StateStage.EXIT;
        }
    }

    GameObject LookForClosestTarget()
    {
        Collider currerntTarget = null;
        // v radiusu kolem sebe zjisti kazdýho potential target
        foreach (Collider target in Physics.OverlapSphere(npc.transform.position, followerEntity.sightDistance, followerEntity.targetMask))
        {
            // najdi vzdalenost k targetu a smìr ke currentTargetu
            float distanceToTarget = Vector3.Distance(npc.transform.position, target.transform.position);
            Vector3 dirToTarget = (target.transform.position - npc.transform.position).normalized;

            // pokud je vzdálenost kratší než attack distance a je blíž než currentTarget tak ho setni jako currentTarget
            if (distanceToTarget < followerEntity.autoDetectRange && (currerntTarget == null || distanceToTarget < Vector3.Distance(currerntTarget.transform.position, target.transform.position)) && !Physics.Raycast(npc.transform.position, dirToTarget, distanceToTarget, followerEntity.obstacleMask))
                currerntTarget = target;

            // pokud je target blíž než currentTarget
            if (currerntTarget == null || distanceToTarget < Vector3.Distance(npc.transform.position, currerntTarget.transform.position))
            {
                // a je v sightAnglu npcka

                if (Vector3.Angle(npc.transform.forward, dirToTarget) < followerEntity.sightAngle / 2)
                {
                    // a není za obstaclem tak ho nastav jako currentTarget
                    if (!Physics.Raycast(npc.transform.position, dirToTarget, distanceToTarget, followerEntity.obstacleMask))
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

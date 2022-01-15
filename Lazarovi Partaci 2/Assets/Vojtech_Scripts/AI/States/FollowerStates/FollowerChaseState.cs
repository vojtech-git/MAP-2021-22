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

        anim.SetBool("isRunning", true);
        anim.SetBool("isAttacking", false);

        base.Enter();
    }

    public override void Update()
    {
        if (target != null)
        {
            base.Update();

            GameObject tempTarget = LookForClosestTarget(); // i kdyz chasuje tak hleda jestli nenajde n�jakej bli��� target
            if (tempTarget != null && tempTarget != target)
                target = tempTarget;

            agent.SetDestination(target.transform.position);

            float distanceToTarget = Vector3.Distance(npc.transform.position, target.transform.position);

            // i v chase by mohl h�zet gran�t

            if (distanceToTarget < followerEntity.sightDistance)
            {
                if (followerEntity.chasingTarget != null)
                {
                    followerEntity.StopCoroutine(followerEntity.chasingTarget);
                    followerEntity.chasingTarget = null;
                }
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
        // v radiusu kolem sebe zjisti kazd�ho potential target
        foreach (Collider target in Physics.OverlapSphere(npc.transform.position, followerEntity.sightDistance, followerEntity.targetMask))
        {
            // najdi vzdalenost k targetu a ke currentTargetu
            float distanceToTarget = Vector3.Distance(npc.transform.position, target.transform.position);

            // pokud je vzd�lenost krat�� ne� attack distance a je bl� ne� currentTarget tak ho setni jako currentTarget
            if (distanceToTarget < followerEntity.attackDistance && (currerntTarget == null || distanceToTarget < Vector3.Distance(currerntTarget.transform.position, target.transform.position)))
                currerntTarget = target;

            // pokud je target bl� ne� currentTarget
            if (currerntTarget == null || distanceToTarget < Vector3.Distance(npc.transform.position, currerntTarget.transform.position))
            {
                // a je v sightAnglu npcka
                Vector3 dirToTarget = (target.transform.position - npc.transform.position).normalized;
                if (Vector3.Angle(npc.transform.forward, dirToTarget) < followerEntity.sightAngle / 2)
                {
                    // a nen� za obstaclem tak ho nastav jako currentTarget
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

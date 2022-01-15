using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FollowerChasePlayerState : State
{
    FollowerEntity followerEntity;
    Transform player;

    public FollowerChasePlayerState(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _target, FollowerEntity _followerEntity, Transform _player) : base(_npc, _agent, _anim)
    {
        player = _player;
        followerEntity = _followerEntity;
    }

    public override void Enter()
    {
        //Debug.Log(npc.gameObject.name + " entered follower chase player state");

        agent.isStopped = false;
        agent.stoppingDistance = 3;

        anim.SetBool("isRunning", true);
        anim.SetBool("isAttacking", false);
        agent.SetDestination(player.position);

        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        GameObject target = LookForClosestTarget();
        if (target != null)
        {
            nextState = new FollowerChaseState(npc, agent, anim, target, followerEntity, player);
            stage = StateStage.EXIT;
        }
        else
        {
            agent.SetDestination(player.position);

            if (Vector3.Distance(npc.transform.position, player.position) < agent.stoppingDistance)
            {
                nextState = new FollowerIdleState(npc, agent, anim, followerEntity, player);
                stage = StateStage.EXIT;
            } 
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    GameObject LookForClosestTarget()
    {
        Collider currerntTarget = null;
        // v radiusu kolem sebe zjisti kazdýho potential target
        foreach (Collider target in Physics.OverlapSphere(npc.transform.position, followerEntity.sightDistance, followerEntity.targetMask))
        {
            // najdi vzdalenost k targetu a ke currentTargetu
            float distanceToTarget = Vector3.Distance(npc.transform.position, target.transform.position);

            // pokud je vzdálenost kratší než auto detect a je blíž než currentTarget tak ho setni jako currentTarget
            if (distanceToTarget < followerEntity.autoDetectRange && (currerntTarget == null || distanceToTarget < Vector3.Distance(currerntTarget.transform.position, target.transform.position)))
                currerntTarget = target;

            // pokud je target blíž než currentTarget
            if (currerntTarget == null || distanceToTarget < Vector3.Distance(npc.transform.position, currerntTarget.transform.position))
            {
                // a je v sightAnglu npcka
                Vector3 dirToTarget = (target.transform.position - npc.transform.position).normalized;
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

}

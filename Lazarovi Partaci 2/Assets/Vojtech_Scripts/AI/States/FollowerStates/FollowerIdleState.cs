using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerIdleState : State
{
    FollowerEntity followerEntity;
    Transform player;

    public FollowerIdleState(GameObject _npc, NavMeshAgent _agent, Animator _anim, FollowerEntity _followerEntity, Transform _player) : base(_npc, _agent, _anim)
    {
        type = StateType.IDLE;
        followerEntity = _followerEntity;
        player = _player;
    }

    public override void Enter()
    {
        //Debug.Log(npc.gameObject.name + " enetered follower idle state");

        agent.isStopped = true;
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);

        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // chase když:
        // vidím target (done)
        // mì nìkdo støelí
        // target vedle mì vystøelí

        GameObject target = LookForClosestTarget();

        if (target != null)
        {
            if (agent.enabled)
            {
                nextState = new FollowerChaseState(npc, agent, anim, target, followerEntity, player);
                stage = StateStage.EXIT; 
            }
        }
        else if (Vector3.Distance(npc.transform.position, player.position) < agent.stoppingDistance + 1f)
        {
            return;
        }
        else
        {
            if (agent.enabled)
            {
                nextState = new FollowerFollowPlayerState(npc, agent, anim, followerEntity, player);
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


            // pokud je vzdálenost kratší než attack distance a je blíž než currentTarget tak ho setni jako currentTarget
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

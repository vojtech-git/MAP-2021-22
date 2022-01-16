using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerAttackState : State
{
    Transform player;
    FollowerEntity followerEntity;
    GameObject target;
    Entity targetEntity;

    public FollowerAttackState(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _target, FollowerEntity _followerEntity, Transform _player) : base(_npc, _agent, _anim)
    {
        type = StateType.IDLE;
        followerEntity = _followerEntity;
        target = _target;
        player = _player;
    }

    public override void Enter()
    {
        //Debug.Log(npc.gameObject.name + " enetered follower attack state");

        if (target != null)
        {
            targetEntity = target.GetComponent<Entity>();
        }

        agent.isStopped = true;
        agent.stoppingDistance = 1;

        anim.SetBool("isAttacking", true);
        anim.SetBool("isRunning", false);

        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (target != null)
        {
            npc.transform.LookAt(new Vector3(target.transform.position.x, npc.transform.position.y, target.transform.position.z));

            if (followerEntity.ReadyToAttack)
            {
                targetEntity.TakeDamage(followerEntity.damage);
                followerEntity.ReadyToAttack = false;

                if (targetEntity.isDead)
                {
                    nextState = new FollowerIdleState(npc, agent, anim, followerEntity, player);
                    stage = StateStage.EXIT;
                    return;
                }
            }

            if (followerEntity.ReadyToThrowGranade && followerEntity.isAGranadeThrower && Vector3.Angle((target.transform.position - npc.transform.position).normalized, npc.transform.forward) < (followerEntity.sightAngle / 2))
            {
                followerEntity.ThrowGranade(target.transform);
                followerEntity.ReadyToThrowGranade = false;
            }

            if (Vector3.Distance(target.transform.position, followerEntity.transform.position) > followerEntity.attackDistance)
            {
                nextState = new FollowerChaseState(npc, agent, anim, target, followerEntity, player);
                stage = StateStage.EXIT;
            }
        }
        else
        {
            nextState = new FollowerIdleState(npc, agent, anim, followerEntity, player);
            stage = StateStage.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

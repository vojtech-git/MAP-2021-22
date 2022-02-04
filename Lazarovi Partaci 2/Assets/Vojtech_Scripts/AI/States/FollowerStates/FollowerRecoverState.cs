using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerRecoverState : State
{
    FollowerEntity followerEntity;
    Transform player;

    public FollowerRecoverState(GameObject _npc, NavMeshAgent _agent, Animator _anim, FollowerEntity _followerEntity, Transform _player) : base(_npc, _agent, _anim)
    {
        followerEntity = _followerEntity;
        player = _player;
    }

    public override void Enter()
    {
        //Debug.Log(npc.gameObject.name + " entered follower recover state");

        base.Enter();

        agent.isStopped = true;
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);
    }

    public override void Update()
    {
        base.Update();
        if (followerEntity.Health == followerEntity.MaxHealth)
        {
            stage = StateStage.EXIT;
            nextState = new FollowerIdleState(npc, agent, anim, followerEntity, player);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

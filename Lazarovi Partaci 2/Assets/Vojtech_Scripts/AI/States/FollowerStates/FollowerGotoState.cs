using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerGotoState : State
{
    FollowerEntity followerEntity;

    public FollowerGotoState(GameObject _npc, NavMeshAgent _agent, Animator _anim, FollowerEntity _followerEntity) : base(_npc, _agent, _anim)
    {
        followerEntity = _followerEntity;

        Debug.Log("enetering folower gotostate" + followerEntity.gameObject.name);
    }

    public override void Enter()
    {
        followerEntity.MoveEntityToPosition();

        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

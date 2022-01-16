using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RocketAttackState : State
{
    RocketEntity rocketEntity;
    GameObject target;

    public RocketAttackState(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _target, RocketEntity _rocketEntity) : base(_npc, _agent, _anim)
    {
        target = _target;
        rocketEntity = _rocketEntity;
    }

    public override void Enter()
    {
        anim.SetBool("isAttacking", true);

        base.Enter();
    }

    public override void Update()
    {
        float distanceToTarget = Vector3.Distance(npc.transform.position, target.transform.position);

        if (distanceToTarget > rocketEntity.sightDistance)
        {
            nextState = new RocketIdleState(npc, agent, anim, rocketEntity);
            stage = StateStage.EXIT;
            return;
        }

        npc.transform.LookAt(target.transform.position);

        if (rocketEntity.ReadyToShootRocket)
        {
            rocketEntity.ShootRocket(target.transform);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;


public class Attack : State
{
    Collider currentTarget;
    EnemyShooterEntity thisEntity;
    Entity currentTargetEntity;

    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, Collider _target) : base(_npc, _agent, _anim, _player)
    {
        currentTarget = _target;
        currentTargetEntity = currentTarget.GetComponent<Entity>();
        thisEntity = _npc.GetComponent<EnemyShooterEntity>();
    }

    public override void Enter()
    {
        Debug.Log("entered Attack state");
        agent.isStopped = true;
        base.Enter();
    }

    public override void Update()
    {
        if (currentTarget == null)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = StateStage.EXIT;
            return;
        }

        base.Update();

        if (Vector3.Distance(currentTarget.transform.position, npc.transform.position) > thisEntity.attackDistance)
        {
            nextState = new Chase(npc, agent, anim, player, currentTarget);
            stage = StateStage.EXIT;
            return;
        }

        if (thisEntity.ReadyToAttack == true)
        {
            currentTargetEntity.TakeDamage(thisEntity.damage);
            thisEntity.ReadyToAttack = false;

            npc.transform.LookAt(new Vector3(currentTarget.transform.position.x, npc.transform.position.y, currentTarget.transform.position.z));
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;


public class Attack : State
{
    Collider currentTarget;
    Entity currentTargetEntity;

    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, Collider _target) : base(_npc, _agent, _anim, _player)
    {
        currentTarget = _target;
        currentTargetEntity = currentTarget.GetComponent<Entity>();
    }

    public override void Enter()
    {
        Debug.Log("entered Attack state");

        agent.isStopped = true;

        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (currentTarget == null)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = StateStage.EXIT;
            return;
        }

        if (currentTarget != null && Vector3.Distance(currentTarget.transform.position, npc.transform.position) > npc.GetComponent<TutorialAI>().attackDistance)
        {
            nextState = new Chase(npc, agent, anim, player, currentTarget);
            stage = StateStage.EXIT;
            return;
        }

        if (npc.GetComponent<TutorialAI>().ReadyToAttack == true)
        {
            currentTargetEntity.TakeDamage(npc.GetComponent<TutorialAI>().damage);
            npc.GetComponent<TutorialAI>().ReadyToAttack = false;

            npc.transform.LookAt(new Vector3(currentTarget.transform.position.x, npc.transform.position.y, currentTarget.transform.position.z));
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    void DealDamage()
    {
        currentTargetEntity.TakeDamage(npc.GetComponent<TutorialAI>().damage);
    }
}

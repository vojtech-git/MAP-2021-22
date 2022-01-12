using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    EnemyShooterEntity entity;

    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        type = StateType.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
        entity = npc.GetComponent<EnemyShooterEntity>();
        agent.isStopped = true;

        Debug.Log("entered Idle state");
    }

    public override void Update()
    {
        base.Update();

        Collider tempTarget = null;
        foreach (Collider target in Physics.OverlapSphere(npc.transform.position, entity.sightDistance, entity.targetMask))
        {
            float distanceToTarget = Vector3.Distance(npc.transform.position, target.transform.position);

            if (distanceToTarget < entity.attackDistance)
            {
                nextState = new Attack(npc, agent, anim, player, target);
                stage = StateStage.EXIT;
                return;
            }
            
            if (tempTarget == null || distanceToTarget < Vector3.Distance(npc.transform.position, tempTarget.transform.position)) // pokud nemam target nemusim kontrolovat jestli je target blíž než current targeet
            {
                Vector3 dirToTarget = (target.transform.position - npc.transform.position).normalized;
                if (Vector3.Angle(npc.transform.forward, dirToTarget) < entity.sightAngle / 2)
                {
                    if (!Physics.Raycast(npc.transform.position, dirToTarget, distanceToTarget, entity.obstacleMask))
                    {
                        tempTarget = target;
                    }
                }
            }
        }

        if (tempTarget != null)
        {
            nextState = new Chase(npc, agent, anim, player, tempTarget);
            stage = StateStage.EXIT;
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

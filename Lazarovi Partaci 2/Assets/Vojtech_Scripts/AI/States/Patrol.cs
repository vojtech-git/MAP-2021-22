using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{

    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        type = StateType.PATROL;
        agent.speed = 2;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            // dorazil do destinace nastav dalsi destinaci
        }
        else
        {
            // if no condition is met that changes our state
            stage = StateStage.UPDATE;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public StateType type;
    protected StateStage stage;
    protected GameObject npc;
    protected NavMeshAgent agent;
    protected Animator anim;
    protected Transform player;
    protected State nextState;

    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        player = _player;

        stage = StateStage.ENTER;
    }

    public virtual void Enter() { stage = StateStage.UPDATE; }
    public virtual void Update() { stage = StateStage.UPDATE; }
    public virtual void Exit() { stage = StateStage.EXIT; }

    public State Process()
    {
        if (stage == StateStage.ENTER) Enter();
        else if (stage == StateStage.UPDATE) Update();
        else if (stage == StateStage.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}

public enum StateType
{
    IDLE, PATROL, CHASE, ATTACK, SLEEP
};

public enum StateStage
{
    ENTER, UPDATE, EXIT
};

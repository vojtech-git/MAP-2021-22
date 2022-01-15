using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FighterAttackState : State
{
    FighterEntity fighterEntity;

    GameObject target;
    Entity targetEntity;

    public FighterAttackState(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _target, FighterEntity _fighterEntity) : base(_npc, _agent, _anim)
    {
        type = StateType.IDLE;
        fighterEntity = _fighterEntity;
        target = _target;
    }

    public override void Enter()
    {
        //Debug.Log(npc.gameObject.name +  " enetered shooter attack state");

        if (target != null)
        {
            targetEntity = target.GetComponent<Entity>(); 
        }
        agent.isStopped = true;
        anim.SetBool("isAttacking", true); 
        anim.SetBool("isRunning", false);

        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (target != null)
        {
            if (Vector3.Distance(target.transform.position, fighterEntity.transform.position) > fighterEntity.attackDistance)
            {
                nextState = new FighterChaseState(npc, agent, anim, target, fighterEntity);
                stage = StateStage.EXIT;
                return;
            }

            npc.transform.LookAt(new Vector3(target.transform.position.x, npc.transform.position.y, target.transform.position.z));

            if (fighterEntity.ReadyToAttack)
            {
                targetEntity.TakeDamage(fighterEntity.damage);
                fighterEntity.ReadyToAttack = false;

                if (targetEntity.isDead)
                {
                    nextState = new FighterIdleState(npc, agent, anim, fighterEntity);
                    stage = StateStage.EXIT;
                    return;
                }
            }

            if (fighterEntity.ReadyToThrowGranade && fighterEntity.isAGranadeThrower)
            {
                ThrowGranade();
                fighterEntity.ReadyToThrowGranade = false;
            }
        }
        else
        {
            nextState = new FighterIdleState(npc, agent, anim, fighterEntity);
            stage = StateStage.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    void ThrowGranade()
    {
        // vymyslet kde se bude granat spawnovat
        GameObject granade = GameObject.Instantiate(fighterEntity.granadePrefab, npc.transform.position + new Vector3(0, 2, 0), npc.transform.rotation);
        Rigidbody rb = granade.GetComponent<Rigidbody>();

        rb.AddForce(npc.transform.forward * 500); // balistick� k�ivka
    }
}

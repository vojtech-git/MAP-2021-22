using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RocketIdleState : State
{
    RocketEntity rocketEntity;

    public RocketIdleState(GameObject _npc, NavMeshAgent _agent, Animator _anim, RocketEntity _rocketEntity) : base(_npc, _agent, _anim)
    {
        rocketEntity = _rocketEntity;
    }

    public override void Enter()
    {
        anim.SetBool("isRunning", false);
        ReturnToPost();
        

        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        GameObject tempTarget = LookForClosestTarget();
        if (tempTarget != null)
        {
            nextState = new RocketAttackState(npc, agent, anim, tempTarget, rocketEntity);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    GameObject LookForClosestTarget()
    {
        Collider currerntTarget = null;
        // v radiusu kolem sebe zjisti kazdýho potential target
        foreach (Collider target in Physics.OverlapSphere(npc.transform.position, rocketEntity.sightDistance, rocketEntity.targetMask))
        {
            // najdi vzdalenost k targetu a ke currentTargetu
            float distanceToTarget = Vector3.Distance(npc.transform.position, target.transform.position);


            // pokud je vzdálenost kratší než attack distance a je blíž než currentTarget tak ho setni jako currentTarget
            if (distanceToTarget < rocketEntity.autoDetectDistance && (currerntTarget == null || distanceToTarget < Vector3.Distance(currerntTarget.transform.position, target.transform.position)))
                currerntTarget = target;

            // pokud je target blíž než currentTarget
            if (currerntTarget == null || distanceToTarget < Vector3.Distance(npc.transform.position, currerntTarget.transform.position))
            {
                // a je v sightAnglu npcka
                Vector3 dirToTarget = (target.transform.position - npc.transform.position).normalized;
                if (Vector3.Angle(npc.transform.forward, dirToTarget) < rocketEntity.sightAngle / 2)
                {
                    // a není za obstaclem tak ho nastav jako currentTarget
                    if (!Physics.Raycast(npc.transform.position, dirToTarget, distanceToTarget, rocketEntity.obstacleMask))
                    {
                        currerntTarget = target;
                    }
                }
            }
        }

        if (currerntTarget != null)
            return currerntTarget.gameObject;
        else
            return null;
    }

    IEnumerator ReturnToPost()
    {
        agent.SetDestination(rocketEntity.post.transform.position);
        yield return new WaitUntil(() => agent.remainingDistance < 1);
        rocketEntity.transform.rotation = rocketEntity.post.transform.rotation;
    }
}

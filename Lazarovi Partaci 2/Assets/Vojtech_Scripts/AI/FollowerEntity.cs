using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowerEntity : Entity
{
    [Header("Stats Modification")]
    public float damage;
    public float attackDelay;

    [Header("View Modifications")]
    public float attackDistance;
    public float sightDistance;
    public float looseSightInstantlyDistance;
    [Range(0, 360)]
    public float sightAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public float outOfRangeTimeToChase;

    NavMeshAgent agent;
    Transform player;

    bool readyToAttack = true;
    bool chasingTargetOutOfRange = false;

    public Transform currentTarget = null;

    Coroutine chaseTargetForSecondVariable;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        FindTarget();
        
        if (currentTarget == null)
        {
            ChasePlayer();
        }
        else
        {
            float distanceToCurrentTarget = Vector3.Distance(transform.position, currentTarget.position);

            if (distanceToCurrentTarget > looseSightInstantlyDistance)
            {
                StopCoroutine(chaseTargetForSecondVariable);
                currentTarget = null;
                chasingTargetOutOfRange = false;
            }
            else if (distanceToCurrentTarget > sightDistance)
            {
                ChaseTarget();
                if (!chasingTargetOutOfRange)
                {
                    chaseTargetForSecondVariable = StartCoroutine(ChaseTargetForSeconds(outOfRangeTimeToChase));
                }
            }
            else if (distanceToCurrentTarget > attackDistance)
            {
                ChaseTarget();
            }
            else if (distanceToCurrentTarget <= attackDistance)
            {
                AttackTarget();
            }
        }
    }

    void FindTarget()
    {
        Collider[] targetsInSightDistance = Physics.OverlapSphere(transform.position, sightDistance, targetMask);

        for (int i = 0; i < targetsInSightDistance.Length; i++) // pro kazdej target v sight rangi
        {
            Transform target = targetsInSightDistance[i].transform; // cashe si target
            Vector3 dirToTarget = (target.position - transform.position).normalized; // uloz si vekor smeru k targetu
            if (Vector3.Angle(transform.forward, dirToTarget) < sightAngle / 2) // jestli ze je v zornym poli
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position); // uloz vzdalenost od targetu

                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask)) // kontroluje jestli cestou k targetu hitnul obstacle
                {
                    if (currentTarget == null || Vector3.Distance(target.position, transform.position) < Vector3.Distance(currentTarget.position, transform.position))
                    {
                        currentTarget = target;
                    }
                }
            }
        }
    }

    void ChaseTarget()
    {
        agent.SetDestination(currentTarget.position);
    }

    void AttackTarget()
    {
        agent.ResetPath();
        
        if(readyToAttack)
        {
            transform.LookAt(new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z)); // mohlo by být nároèný ale kdyz je to jenom currentTarget tak se naklání follower dopøedu

            currentTarget.gameObject.GetComponent<Entity>().TakeDamage(damage);
            readyToAttack = false;
            StartCoroutine(DelayAttack(attackDelay));
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    public override void Die()
    {
        // neumøe poze ho downne a zaène si regenerovat životy
        // animation downed true
        // boxcollider turn off aby do nej nikdo nestrilel
        // StartCorutine(regenHealth())
    }

    IEnumerator DelayAttack(float delayBetweenAttacks)
    {
        yield return new WaitForSeconds(delayBetweenAttacks);
        readyToAttack = true;
    }

    IEnumerator ChaseTargetForSeconds(float outOfRangeChaseTime)
    {
        chasingTargetOutOfRange = true;
        yield return new WaitForSeconds(outOfRangeChaseTime);
        currentTarget = null;
        chasingTargetOutOfRange = false;
    }
}

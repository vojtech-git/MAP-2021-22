using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyShooterEntity : EnemyEntity
{
    [Header("Stats Modification")]
    public float damage = 10;
    public int attackDelayInSeconds = 1;

    [Header("View Modifications")]
    public float attackDistance = 3;
    public float sightDistance = 20;
    public float looseSightInstantlyDistance = 50;
    [Range(0, 360)]
    public float sightAngle = 100;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public float outOfRangeTimeToChase = 5;

    NavMeshAgent agent;
    Transform player;
    Animator anim;

    State currentState;
    private bool readyToAttack = true;
    public bool ReadyToAttack
    {
        get { return readyToAttack; }
        set
        {
            if (value == false)
            {
                readyToAttack = false;
                StartCoroutine(ResetAttackAfterDelay(attackDelayInSeconds));
            }
            else
            {
                readyToAttack = value;
            }
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>(); 
        currentState = new Idle(this.gameObject, agent, anim, player);
    }

    void Update()
    {
        currentState = currentState.Process();
    }

    public override void Die()
    {

        base.Die();
    }

    IEnumerator ResetAttackAfterDelay(float delayBetweenAttacks)
    {
        yield return new WaitForSeconds(delayBetweenAttacks);
        ReadyToAttack = true;
    }
}

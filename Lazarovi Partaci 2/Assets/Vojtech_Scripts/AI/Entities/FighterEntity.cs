using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class FighterEntity : Entity
{
    [Header("Stats Modification")]
    public float damage = 10;
    public int attackDelayInSeconds = 1;
    public bool isAGranadeThrower;
    public int granadeDelayInSeconds = 5;

    [Header("View Modifications")]
    public float attackDistance = 3;
    public float autoDetectRange = 10;
    public float sightDistance = 20;
    public float outOfRangeTimeToChase = 5;
    public float looseSightInstantlyDistance = 50;
    [Range(0, 360)]
    public float sightAngle = 100;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [Header("Other")]
    public int entityId;
    public GameObject granadePrefab;
    public GameObject[] ItemsToDropPrefabs;

    private NavMeshAgent agent; // mohl bych pøiøadit v inspektoru uvnitø prefaby (agenta, animator)
    private Animator anim;
    private State currentState;
    public delegate void StoppedChasingDelegate();
    public Coroutine chasingTarget;
    private bool readyToAttack = true;
    public bool ReadyToAttack
    {
        get { return readyToAttack; }
        set
        {
            if (value == false)
            {
                if (readyToAttack == true)
                    StartCoroutine(DelayAttack(attackDelayInSeconds));

                readyToAttack = false;
            }
            else
            {
                readyToAttack = value;
            }
        }
    }
    private bool readyToThrowGranade = true;
    public bool ReadyToThrowGranade
    {
        get { return readyToThrowGranade; }
        set
        {
            if (value == false)
            {
                if (readyToThrowGranade == true)
                    StartCoroutine(DelayGranade(granadeDelayInSeconds));

                readyToThrowGranade = false;
            }
            else
            {
                readyToThrowGranade = value;
            }
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        currentState = new FighterIdleState(gameObject, agent, anim, this);
    }

    void Update()
    {
        currentState = currentState.Process();
    }

    public override void Die()
    {
        if (!isDead)
        {
            QuestingSystem.ProgressQuests(GoalType.Kill, entityId);
            DropLoot();
            base.Die();
        }
    }

    public void ChaseTargetOutOfRange(StoppedChasingDelegate stoppedChasingAction)
    {
        chasingTarget = StartCoroutine(ChaseTargetOutOfRange(outOfRangeTimeToChase, stoppedChasingAction));
    }

    IEnumerator DelayAttack(float delayBetweenAttacks)
    {
        yield return new WaitForSeconds(delayBetweenAttacks);
        ReadyToAttack = true;
    }

    void DropLoot()
    {
        int random = Random.Range(0, ItemsToDropPrefabs.Length - 1);
        GameObject droppedLoot = Instantiate(ItemsToDropPrefabs[random], transform.position, transform.rotation);
        droppedLoot.transform.Rotate(-90, 0, 0);
    }

    IEnumerator DelayGranade(float delayBetweenGranade)
    {
        yield return new WaitForSeconds(delayBetweenGranade);
        readyToThrowGranade = true;
    }
    IEnumerator ChaseTargetOutOfRange(float timeToChase, StoppedChasingDelegate stoppedChasingAction)
    {
        yield return new WaitForSeconds(timeToChase);
        stoppedChasingAction();
    }
}

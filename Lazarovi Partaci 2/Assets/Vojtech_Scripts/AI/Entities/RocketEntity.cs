using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RocketEntity : Entity
{
    [Header("Stats Modification")]
    public float damage = 10;
    public int attackDelayInSeconds = 1;

    [Header("View Modifications")]
    public float autoDetectDistance = 10;
    public float sightDistance = 20; // útoèí hned jak vidí
    [Range(0, 360)]
    public float sightAngle = 100;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [Header("Other")]
    public int entityId;
    public GameObject RocketPrefab;
    public GameObject[] ItemsToDropPrefabs;
    public GameObject post;

    private NavMeshAgent agent; // mohl bych pøiøadit v inspektoru uvnitø prefaby (agenta, animator)
    private Animator anim;
    private State currentState;
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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        currentState = new RocketIdleState(gameObject, agent, anim, this);
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

    void DropLoot()
    {
        int random = Random.Range(0, ItemsToDropPrefabs.Length - 1);
        GameObject droppedLoot = Instantiate(ItemsToDropPrefabs[random], transform.position, transform.rotation);
        droppedLoot.transform.Rotate(-90, 0, 0);
    }

    IEnumerator DelayAttack(float delayBetweenAttacks)
    {
        yield return new WaitForSeconds(delayBetweenAttacks);
        ReadyToAttack = true;
    }
}

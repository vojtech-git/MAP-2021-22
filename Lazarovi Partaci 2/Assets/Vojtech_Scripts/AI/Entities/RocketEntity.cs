using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RocketEntity : Entity
{
    [Header("Stats Modification")]
    public float rocketDamage = 10;
    public float rocketSpeed = 0.05f;
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
    public GameObject rocketPrefab;
    public GameObject rocketSpawnPoint;
    public GameObject[] itemsToDropPrefabs;

    private NavMeshAgent agent; // mohl bych pøiøadit v inspektoru uvnitø prefaby (agenta, animator)
    private Animator anim;
    private State currentState;
    private bool readyToShootRocket = true;
    public bool ReadyToShootRocket
    {
        get { return readyToShootRocket; }
        set
        {
            if (value == false)
            {
                if (readyToShootRocket == true)
                    StartCoroutine(DelayAttack(attackDelayInSeconds));

                readyToShootRocket = false;
            }
            else
            {
                readyToShootRocket = value;
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
            QuestingManager.ProgressQuests(GoalType.Kill, entityId);
            DropLoot();
            base.Die(); 
        }
    }

    public void ShootRocket(Transform target)
    {
        rocketSpawnPoint.transform.LookAt(target);
        Rocket rocket = Instantiate(rocketPrefab, rocketSpawnPoint.transform.position, rocketSpawnPoint.transform.rotation).GetComponent<Rocket>();
        rocket.rocketDamage = rocketDamage;
        rocket.rocketSpeed = rocketSpeed;

        ReadyToShootRocket = false;
    }

    void DropLoot()
    {
        int random = Random.Range(0, itemsToDropPrefabs.Length - 1);
        GameObject droppedLoot = Instantiate(itemsToDropPrefabs[random], transform.position, transform.rotation);
        droppedLoot.transform.Rotate(-90, 0, 0);
    }

    IEnumerator DelayAttack(float delayBetweenAttacks)
    {
        yield return new WaitForSeconds(delayBetweenAttacks);
        ReadyToShootRocket = true;
    }
}

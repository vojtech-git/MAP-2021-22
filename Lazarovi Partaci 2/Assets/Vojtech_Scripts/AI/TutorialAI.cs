using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class TutorialAI : MonoBehaviour
{
    [Header("Stats Modification")]
    public float damage;
    public int attackDelayInSeconds;

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

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = new Idle(gameObject, agent, anim, player);
    }

    private void Update()
    {
        currentState = currentState.Process();

        // if event FrindlyShot is triggered set currentState na chase a posli tam toho kdo vystrelil jako target. (Jestliže byl target v rangi)
    }

    void ShotHeard(GameObject whoShot)
    {

    }

    IEnumerator ResetAttackAfterDelay(float delayBetweenAttacks)
    {
        yield return new WaitForSeconds(delayBetweenAttacks);
        ReadyToAttack = true;
    }
}

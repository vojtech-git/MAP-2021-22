using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class FollowerEntity : Entity
{
    [Header("Stats Modification")]
    public float damage = 10;
    public int attackDelayInSeconds = 1;
    public bool isAGranadeThrower;
    public int granadeDelayInSeconds = 5;

    [Header("View Modifications")]
    public float attackDistance = 1.5f;
    public float autoDetectRange = 10;
    public float sightDistance = 20;
    public float outOfRangeTimeToChase = 5;
    public float looseSightInstantlyDistance = 50;
    [Range(0, 360)]
    public float sightAngle = 100;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [Header("Other")]
    public GameObject granadePrefab;
    public GameObject granadeSpawnPos;
    public float speedOfGranade = 15;

    [HideInInspector] public bool scriptedSequencePlaying;
    public NavMeshAgent agent; // mohl bych pøiøadit v inspektoru uvnitø prefaby (agenta, animator)
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
                    StartCoroutine(DelayAttack(attackDelayInSeconds));

                readyToThrowGranade = false;
            }
            else
            {
                readyToThrowGranade = value;
            }
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        if (!scriptedSequencePlaying)
        {
            currentState = new FollowerFollowPlayerState(gameObject, agent, anim, this, GameObject.FindGameObjectWithTag("Player").transform);
            Health = MaxHealth;
        }
    }

    void Update()
    {
        if (enabled && !scriptedSequencePlaying)
        {
            currentState = currentState.Process();
        }
    }

    public override void Die()
    {
        if (!isDead)
        {
            isDead = true;
            currentState.Exit();
            currentState = new FollowerRecoverState(gameObject, agent, anim, this, GameObject.FindGameObjectWithTag("Player").transform);
            StartCoroutine(RecoverHealth());
        }        
    }
    // co se má stát když umøe follower pøes sequenci (GoalAction)
    public void StoryDie()
    {
        Destroy(gameObject);
    }
    public void ReturnToFollowerState()
    {
        currentState = new FollowerFollowPlayerState(gameObject, agent, anim, this, GameObject.FindGameObjectWithTag("Player").transform);
    }

    #region Granade Throwing
    public void ThrowGranade(Transform target)
    {
        granadeSpawnPos.transform.LookAt(target);
        float? angle = RotateGranadeSpawnPoint(target);

        if (angle != null)
        {
            GameObject granade = Instantiate(granadePrefab, granadeSpawnPos.transform.position, granadeSpawnPos.transform.rotation);
            granade.GetComponent<Rigidbody>().velocity = speedOfGranade * granade.transform.forward;

            ReadyToThrowGranade = false;
        }
    }

    float? RotateGranadeSpawnPoint(Transform target)
    {
        transform.LookAt(target);
        float? angle = CalculateAngle(target);

        if (angle != null)
        {
            granadeSpawnPos.transform.localEulerAngles = new Vector3(360f - (float)angle, 0, 0);
        }

        return angle;
    }
    float? CalculateAngle(Transform target)
    {
        Vector3 targetDir = target.transform.position - granadeSpawnPos.transform.position;
        float y = targetDir.y;
        targetDir.y = 0;
        float x = targetDir.magnitude;
        float gravity = 9.81f;
        float speedSqr = speedOfGranade * speedOfGranade;
        float underSquareRoot = (speedSqr * speedSqr) - gravity * (gravity * x * x + 2 * y + speedSqr);

        if (underSquareRoot >= 0f)
        {
            float root = Mathf.Sqrt(underSquareRoot);
            float angle = speedSqr - root;

            return (Mathf.Atan2(angle, gravity * x) * Mathf.Rad2Deg);
        }

        return null;
    } 
    #endregion

    // corutine call methods
    public void ChaseTargetOutOfRange(StoppedChasingDelegate stoppedChasingAction)
    {
        chasingTarget = StartCoroutine(ChaseTargetOutOfRange(outOfRangeTimeToChase, stoppedChasingAction));
    }

    #region Corutines
    IEnumerator DelayAttack(float delayBetweenAttacks)
    {
        yield return new WaitForSeconds(delayBetweenAttacks);
        ReadyToAttack = true;
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
    public IEnumerator GoToPosition(Vector3 gotoPosition)
    {
        Debug.Log("agent going to pos " + gotoPosition);

        agent.isStopped = false;
        agent.stoppingDistance = 1f;
        if (!agent.SetDestination(gotoPosition))
        {
            Debug.Log("destination not set succesfully");
        }
        anim.SetBool("isRunning", true);
        anim.SetBool("isAttacking", false);

        yield return new WaitUntil(() => (transform.position - gotoPosition).magnitude < agent.stoppingDistance);

        agent.isStopped = true;
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);

        Debug.Log("at pos" + gameObject.name);
    }
    IEnumerator RecoverHealth()
    {
        while (Health < MaxHealth)
        {
            AddHealth(MaxHealth / 100);
            yield return new WaitForSeconds(0.1f);
        }

        isDead = false;
    }
    #endregion
}

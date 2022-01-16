using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
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

    private NavMeshAgent agent; // mohl bych p�i�adit v inspektoru uvnit� prefaby (agenta, animator)
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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        currentState = new FollowerIdleState(gameObject, agent, anim, this, GameObject.FindGameObjectWithTag("Player").transform);
        Health = MaxHealth;
    }

    void Update()
    {
        currentState = currentState.Process();
    }

    public override void Die()
    {
        if (!isDead)
        {
            base.Die();
        }        
    }

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

    public void ChaseTargetOutOfRange(StoppedChasingDelegate stoppedChasingAction)
    {
        chasingTarget = StartCoroutine(ChaseTargetOutOfRange(outOfRangeTimeToChase, stoppedChasingAction));
    }

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(NavMeshAgent))]
public class FighterEntity : Entity
{
    [Header("Stats Modification")]
    public float damage = 10f;
    public float granadeDamage = 20f;
    public float speedOfGranade = 15f;
    public float granadeBlastRadius = 5f;
    public int attackDelayInSeconds = 1;
    public bool isAGranadeThrower;
    public int granadeDelayInSeconds = 5;

    [Header("View Modifications")]
    public float attackDistance = 3f;
    public float autoDetectDistance = 10f;
    public float sightDistance = 20f;
    public float outOfRangeTimeToChase = 5f;
    public float looseSightInstantlyDistance = 50f;
    [Range(0, 360)]
    public float sightAngle = 100f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [Header("Other")]
    public int entityId;
    public GameObject granadePrefab;
    public GameObject granadeSpawnPos;
    public GameObject[] ItemsToDropPrefabs;
    Vector3 postPosition;

    public Coroutine runningToPost;
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

    private void Awake()
    {
        // sight distance by nemela byt mensi nez autoDetect protoze CheckSphere vzdy probíhá na vzdálenost sightDistance.
        if (sightDistance < autoDetectDistance)
        {
            sightDistance = autoDetectDistance;
        }

        // mohlo by se nastavit špatnì kdyby se mu naèetla pozice ze saveloadu pred tímhle
        postPosition = transform.position;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        currentState = new FighterIdleState(gameObject, agent, anim, this);

        Health = MaxHealth;
    }

    void Update()
    {
        if (enabled)
        {
            currentState = currentState.Process();
        }    
    }

    public override void Die()
    {
        if (!isDead)
        {
            QuestingManager.OnPointGained(GoalType.Kill, entityId);
            DropLoot();
            base.Die();
        }
    }

    public void ThrowGranade(Transform target)
    {
        granadeSpawnPos.transform.LookAt(target);
        float? angle = RotateGranadeSpawnPoint(target);

        if (angle != null)
        {
            Granade granade = Instantiate(granadePrefab, granadeSpawnPos.transform.position, granadeSpawnPos.transform.rotation).GetComponent<Granade>();
            granade.rb.velocity = speedOfGranade * granade.transform.forward;
            granade.granadeDamage = granadeDamage;
            granade.granadeExplosionRadius = granadeBlastRadius;

            ReadyToThrowGranade = false;
        }
    }

    float? RotateGranadeSpawnPoint(Transform target)
    {
        float? angle = CalculateAngle(target);

        if (angle != null)
        {
            granadeSpawnPos.transform.localEulerAngles = new Vector3(360f - (float)angle, granadeSpawnPos.transform.localEulerAngles.y, granadeSpawnPos.transform.localEulerAngles.z);
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

    void DropLoot()
    {
        int random = Random.Range(0, ItemsToDropPrefabs.Length - 1);
        GameObject droppedLoot = Instantiate(ItemsToDropPrefabs[random], transform.position, transform.rotation);
        droppedLoot.transform.Rotate(-90, 0, 0);
    }

    public void ChaseTargetOutOfRange(StoppedChasingDelegate stoppedChasingAction)
    {
        chasingTarget = StartCoroutine(ChaseTargetOutOfRange(outOfRangeTimeToChase, stoppedChasingAction));
    }
    public void ReturnEntityToPost()
    {
        runningToPost = StartCoroutine(ReturnToPost());
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
    IEnumerator ReturnToPost()
    {
        agent.SetDestination(postPosition);
        anim.SetBool("isRunning", true);
        anim.SetBool("isAttacking", false);

        yield return new WaitUntil(() => agent.remainingDistance < agent.stoppingDistance + 1);

        agent.isStopped = true;
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sightDistance); 
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, autoDetectDistance);
    }
}

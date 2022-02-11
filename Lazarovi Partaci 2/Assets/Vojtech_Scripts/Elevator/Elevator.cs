using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
public class Elevator : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject topMarker;
    public GameObject bottomMarker;

    [Header("Specifications")]
    public float speed;
    public Door[] doorsToLockWhenMoving;

    List<NavMeshAgent> agentsOnElevator = new List<NavMeshAgent>();
    bool traveling;
    bool goingUp;
    Coroutine travelingCorutine;

    float startTime;
    float journeyLength;
    Vector3 startPosition;
    Vector3 endPosition;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.TryGetComponent(out NavMeshAgent agent))
        {
            agentsOnElevator.Add(agent);
        }

        other.transform.parent = this.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out NavMeshAgent agent))
        {
            agentsOnElevator.Remove(agent);
        }

        other.transform.parent = null;
    }

    private void FixedUpdate()
    {
        if (traveling)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            this.transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

            if (this.transform.position == endPosition)
            {
                StopJourney();
            }
        }
    }

    public void UpdateStartJourney()
    {
        if (enabled)
        {
            traveling = true;
            foreach (NavMeshAgent navMeshAgent in agentsOnElevator)
            {
                navMeshAgent.enabled = false;
            }
            foreach (Door door in doorsToLockWhenMoving)
            {
                door.enabled = false;
                door.CloseDoor();
            }

            startPosition = transform.position;
            if (goingUp)
                endPosition = topMarker.transform.position;
            else
                endPosition = bottomMarker.transform.position;
            journeyLength = (startPosition - endPosition).magnitude;

            startTime = Time.time;

            goingUp = !goingUp;  
        }
    }

    void StopJourney()
    {
        traveling = false;

        foreach (NavMeshAgent navMeshAgent in agentsOnElevator)
        {
            navMeshAgent.enabled = true;
        }
        foreach (Door door in doorsToLockWhenMoving)
        {
            door.enabled = true;
        }
    }

    #region oldLogic
    IEnumerator Travel(Vector3 startPosition, Vector3 endPosition, float journeyLength, float startTime)
    {
        WaitForSeconds wait = new WaitForSeconds(0.0001f);

        while (traveling)
        {
            yield return wait;

            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            this.transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

            if (this.transform.position == endPosition)
            {
                StopJourney();
            }

        }
    }
    public void StartJourney()
    {
        traveling = true;
        StopAllCoroutines();

        foreach (NavMeshAgent navMeshAgent in agentsOnElevator)
        {
            navMeshAgent.enabled = false;
        }

        Vector3 startPosition = transform.position;
        Vector3 endPosition;
        if (goingUp)
            endPosition = topMarker.transform.position;
        else
            endPosition = bottomMarker.transform.position;
        float journeyLength = (startPosition - endPosition).magnitude;

        StartCoroutine(Travel(startPosition, endPosition, journeyLength, Time.time));

        Debug.Log("starting elevator" + startPosition + endPosition + journeyLength + Time.time + goingUp);

        goingUp = !goingUp;
    }
    #endregion
}

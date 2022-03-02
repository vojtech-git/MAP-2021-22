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
    public ElevatorSwitch[] switches;

    List<NavMeshAgent> agentsOnElevator = new List<NavMeshAgent>();
    Dictionary<Collider, Transform> parents = new Dictionary<Collider, Transform>();
    bool traveling;
    bool goingDown;
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

        parents.Add(other, other.transform.parent);

        other.transform.parent = this.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out NavMeshAgent agent))
        {
            agentsOnElevator.Remove(agent);
        }

        other.transform.parent = parents[other];
        parents.Remove(other);
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
            foreach (ElevatorSwitch elevatorSwitch in switches)
            {
                if (goingDown)
                {
                    elevatorSwitch.arrowDown.gameObject.SetActive(true);
                    elevatorSwitch.arrowUp.gameObject.SetActive(false);
                }
                else
                {
                    elevatorSwitch.arrowUp.gameObject.SetActive(true);
                    elevatorSwitch.arrowDown.gameObject.SetActive(false);
                }
            }

            startPosition = transform.position;
            if (goingDown)
                endPosition = bottomMarker.transform.position;
            else
                endPosition = topMarker.transform.position;

            journeyLength = (startPosition - endPosition).magnitude;
            startTime = Time.time;
            goingDown = !goingDown;
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
            door.OpenDoor();
        }
        foreach (ElevatorSwitch elevatorSwitch in switches)
        {
            elevatorSwitch.arrowDown.gameObject.SetActive(false);
            elevatorSwitch.arrowUp.gameObject.SetActive(false);
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
        if (goingDown)
            endPosition = topMarker.transform.position;
        else
            endPosition = bottomMarker.transform.position;
        float journeyLength = (startPosition - endPosition).magnitude;

        StartCoroutine(Travel(startPosition, endPosition, journeyLength, Time.time));

        Debug.Log("starting elevator" + startPosition + endPosition + journeyLength + Time.time + goingDown);

        goingDown = !goingDown;
    }
    #endregion
}

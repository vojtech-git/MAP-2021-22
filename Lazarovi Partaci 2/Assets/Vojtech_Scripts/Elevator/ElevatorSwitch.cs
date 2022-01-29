using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{
    public Elevator elevator;

    public void StartElevator()
    {
        elevator.StartJourney();
    }
}

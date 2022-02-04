using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : Interactable
{
    public Elevator elevator;

    public override void Interact()
    {
        elevator.UpdateStartJourney();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorSwitch : Interactable
{
    [Header("Elevator")]
    public Elevator elevator;
    public Image arrowUp;
    public Image arrowDown;

    public override void Interact()
    {
        elevator.UpdateStartJourney();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRay : MonoBehaviour
{
    public float interactionDistance;
    public GameObject mainCamera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, interactionDistance))
            {
                if (hit.transform.TryGetComponent(out ElevatorSwitch elevatorSwitch))
                {
                    elevatorSwitch.StartElevator();
                }
            }
        }
    }
}

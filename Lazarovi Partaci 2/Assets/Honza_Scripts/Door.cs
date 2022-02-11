using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Animator doorAnim;

    List<Collider> collidersOpeningDoors = new List<Collider>();

    void OnTriggerEnter(Collider other)
    {
        collidersOpeningDoors.Add(other);

        if (enabled)
        {
            OpenDoor();
        }
    }

    void OnTriggerExit(Collider other)
    {
        collidersOpeningDoors.Remove(other);

        if (enabled && collidersOpeningDoors.Count == 0)
        {
            CloseDoor();
        }
    }

    public void CloseDoor()
    {
        //Debug.Log("closing doors");
        doorAnim.SetBool("open", false);
    }

    public void OpenDoor()
    {
        //Debug.Log("opening doors");
        doorAnim.SetBool("open", true);
    }
}

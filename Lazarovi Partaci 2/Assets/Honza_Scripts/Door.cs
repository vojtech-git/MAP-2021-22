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
        if (enabled)
        { 
            collidersOpeningDoors.Add(other);

            doorAnim.SetTrigger("CloseOpen");
        }
    }

    void OnTriggerExit(Collider other)
    {
        collidersOpeningDoors.Remove(other);

        if (enabled)
        {
            if (collidersOpeningDoors.Count == 0)
            {
                doorAnim.SetTrigger("OpenClose");
            } 
        }
    }
}

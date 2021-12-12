using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRoom : MonoBehaviour
{
    public GameObject activeRoom;
    private void OnTriggerStay(Collider other)
    {
        activeRoom.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        activeRoom.SetActive(false);
    }
}

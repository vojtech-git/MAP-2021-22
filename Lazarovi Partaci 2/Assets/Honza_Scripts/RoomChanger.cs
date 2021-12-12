using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    public GameObject Room2;
    public GameObject Room1;
    public GameObject yy;
    private void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        yy.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Room2.activeInHierarchy == true)
            {
                Room2.SetActive(false);
                Room1.SetActive(true);
            }
            else
            {
                Room2.SetActive(true);
                Room1.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        yy.SetActive(false);
    }
}

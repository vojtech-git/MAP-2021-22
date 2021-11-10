using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Room2;
    public GameObject Room1;
    public GameObject yy;
    private void OnTriggerStay(Collider other)
    {

        yy.SetActive(true);
        if (Input.GetKey(KeyCode.E))
        {
            Room2.SetActive(true);
            Room1.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Room2.SetActive(false);
            Room1.SetActive(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        yy.SetActive(false);
    }
}

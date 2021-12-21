using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbechCam : MonoBehaviour
{
    public GameObject workbenchCam;
    public GameObject player;
    public GameObject marker;

    private void OnTriggerStay(Collider other)
    {

        marker.SetActive(true);
        if (Input.GetKey(KeyCode.E))
        {
            EnableCam();
        }

    }
    void EnableCam()
    {
        workbenchCam.SetActive(true);
        player.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            workbenchCam.SetActive(false);
            player.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

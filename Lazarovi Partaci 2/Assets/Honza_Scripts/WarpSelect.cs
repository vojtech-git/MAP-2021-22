using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class WarpSelect : MonoBehaviour
{
    public GameObject warpCam;
    public GameObject player;
    public GameObject marker;
    public PlayableDirector directorBridge;

    public Material skybox1;
    public Material skybox2;
    public Material skybox3;

    public bool system1;
    public bool system2;
    public bool system3;

    public bool warping = false;
    public bool change = false;

    private void OnTriggerStay(Collider other)
    {
        if (warping == false)
        {
            marker.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                EnableCam();
            }
        }
    }
    void EnableCam()
    {
        warpCam.SetActive(true);
        player.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Update()
    {
            if (Input.GetButtonDown("Cancel"))
            {
                ExitThisMenu();
            }
            if (change == true)
            {
                ChangeSkybox();
                change = false;
            }
    }
    public void WarpToSystem1()
    {
        system1 = true;
        system2 = false;
        system3 = false;
        directorBridge.Play();
        ExitThisMenu();
    }
    public void WarpToSystem2()
    {
        system1 = false;
        system2 = true;
        system3 = false;
        directorBridge.Play();
        ExitThisMenu();
    }
    public void WarpToSystem3()
    {
        system1 = false;
        system2 = false;
        system3 = true;
        directorBridge.Play();
        ExitThisMenu();

    }

    public void ChangeSkybox()
    {
        if(system1 == true)
        {
            RenderSettings.skybox = skybox1;
        }
        if (system2 == true)
        {
            RenderSettings.skybox = skybox2;
        }
        if (system3 == true)
        {
            RenderSettings.skybox = skybox3;
        }
    }
    void ExitThisMenu()
    {
        warpCam.SetActive(false);
        player.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnTriggerExit(Collider other)
    {
        marker.SetActive(false);
    }
}

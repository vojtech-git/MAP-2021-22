using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchCam : MonoBehaviour
{
    public GameObject workbenchCam;
    public GameObject player;
    public GameObject marker;
    public GameObject mods;
    public GameObject[] canvases;

    public float lerpTime = 1;

    public Vector3 defaultPos;
    public Quaternion defaultRot;

    public Vector3 magsPos;
    public Quaternion magsRot;

    public Vector3 scopesPos;
    public Quaternion scopesRot;

    public Vector3 muzzlesPos;
    public Quaternion muzzlesRot;

    public Vector3 specialPos;
    public Quaternion specialRot;
    public void Default()
    {
        workbenchCam.transform.position = Vector3.Lerp(workbenchCam.transform.position, defaultPos, lerpTime);
        workbenchCam.transform.rotation = Quaternion.Lerp(workbenchCam.transform.rotation, defaultRot, lerpTime);
    }
    public void Mags()
    {
        workbenchCam.transform.position = Vector3.Lerp(workbenchCam.transform.position, magsPos, lerpTime);
        workbenchCam.transform.rotation = Quaternion.Lerp(workbenchCam.transform.rotation, magsRot, lerpTime);
    }
    public void Scopes()
    {
        workbenchCam.transform.position = Vector3.Lerp(workbenchCam.transform.position, scopesPos, lerpTime);
        workbenchCam.transform.rotation = Quaternion.Lerp(workbenchCam.transform.rotation, scopesRot, lerpTime);
    }
    public void Muzzles()
    {
        workbenchCam.transform.position = Vector3.Lerp(workbenchCam.transform.position, muzzlesPos, lerpTime);
        workbenchCam.transform.rotation = Quaternion.Lerp(workbenchCam.transform.rotation, muzzlesRot, lerpTime);
    }
    public void Special()
    {
        workbenchCam.transform.position = Vector3.Lerp(workbenchCam.transform.position, specialPos, lerpTime);
        workbenchCam.transform.rotation = Quaternion.Lerp(workbenchCam.transform.rotation, specialRot, lerpTime);
    }

    private void OnTriggerStay(Collider other)
    {
        marker.SetActive(true);
        if (Input.GetKey(KeyCode.E))
        {
            Default();
            EnableCam();
        }
    }
    void EnableCam()
    {
        mods.SetActive(true);
        workbenchCam.SetActive(true);
        player.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnTriggerExit(Collider other)
    {
        marker.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ExitThisMenu();
        }
    }
    void ExitThisMenu()
    {
        foreach (var item in canvases)
        {
            item.SetActive(false);
        }
        workbenchCam.SetActive(false);
        player.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

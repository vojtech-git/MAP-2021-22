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

    Quaternion currentRot;
    Vector3 currentPos;
    private void Awake()
    {
        currentPos = defaultPos;
        currentRot = defaultRot;
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
        ChangeCamera(currentPos,currentRot);
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
    //Cam pozice
    void ChangeCamera(Vector3 targetPos, Quaternion targetRot)
    {
        if (workbenchCam.transform.position != targetPos)
        {
            workbenchCam.transform.position = Vector3.Lerp(workbenchCam.transform.position, targetPos, lerpTime);
            workbenchCam.transform.rotation = Quaternion.Lerp(workbenchCam.transform.rotation, targetRot, lerpTime);
        }
    }
    public void Default()
    {
        currentPos = defaultPos;
        currentRot = defaultRot;
    }
    public void Mags()
    {
        currentPos = magsPos;
        currentRot = magsRot;
    }
    public void Scopes()
    {
        currentPos = scopesPos;
        currentRot = scopesRot;
    }
    public void Muzzles()
    {
        currentPos = muzzlesPos;
        currentRot = muzzlesRot;
    }
    public void Special()
    {
        currentPos = specialPos;
        currentRot = specialRot;
    }
}

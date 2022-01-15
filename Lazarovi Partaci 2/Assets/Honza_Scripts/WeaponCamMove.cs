using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCamMove : MonoBehaviour
{
    public float mouseSen = 100f;
    public Transform weaponCam;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSen * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSen * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        weaponCam.Rotate(Vector3.up * mouseX);
    }
}

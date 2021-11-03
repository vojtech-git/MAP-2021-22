using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIrotate : MonoBehaviour
{
    [Header("Rotate to mouse")]
    public Transform orb;
    public float radius;
    public Camera playerCamera;

    private Transform pivot;

    void Start()
    {
        pivot = orb.transform;
        transform.parent = pivot;
        transform.position += Vector3.up * radius;
    }

    void Update()
    {
        Vector3 orbVector = playerCamera.WorldToScreenPoint(orb.position);
        orbVector = Input.mousePosition - orbVector;
        float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;

        pivot.position = orb.position;
        pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public GameObject toSpin;
    [Header("Spin")]
    public float rotateX = 0;
    public float rotateY = 0;
    public float rotateZ = 0;
    [Header("Move")]
    public float speed = 0f;
    public float heightX = 0f;
    public float heightY = 0f;
    public float heightZ = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        toSpin.transform.Rotate(rotateX,rotateY,rotateZ, Space.Self);
    }
    void Update()
    {
        Vector3 pos = transform.position;
        if (heightX > 0)
        {
            float newX = Mathf.Sin(Time.deltaTime * speed);
            transform.position = new Vector3(pos.x, newX, pos.z) * heightX;
        }

        if (heightX > 0)
        {
            float newY = Mathf.Sin(Time.deltaTime * speed);
            transform.position = new Vector3(pos.x, newY, pos.z) * heightY;
        }
        if (heightX > 0)
        {
            float newZ = Mathf.Sin(Time.deltaTime * speed);
            transform.position = new Vector3(pos.x, newZ, pos.z) * heightZ;
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOfAsteroids : MonoBehaviour
{
     Transform tr;

    Vector3 rotation;
    public float rotationValue = 50f;
    
  
    void Awake(){
        tr = transform;
    }
    void Start()
    {
        rotation.x = Random.Range(-rotationValue, rotationValue);
        rotation.y = Random.Range(-rotationValue, rotationValue);
        rotation.z = Random.Range(-rotationValue, rotationValue);
    }


    void Update()
    {
       tr.Rotate(rotation * Time.deltaTime); 
    }
}

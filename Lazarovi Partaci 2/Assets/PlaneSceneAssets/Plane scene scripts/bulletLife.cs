using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletLife : MonoBehaviour
{
    // Start is called before the first frame update
     public float TimeToLive = 1f;
    void Start()
    {
         
    
         Destroy(gameObject, TimeToLive);
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

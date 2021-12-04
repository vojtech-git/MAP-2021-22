using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warningBlink : MonoBehaviour
{
    public PlaneHealth planeHealth;
    public Image warning;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(planeHealth.health <= 10){
            warning.enabled=true;
            
        }
    }
}

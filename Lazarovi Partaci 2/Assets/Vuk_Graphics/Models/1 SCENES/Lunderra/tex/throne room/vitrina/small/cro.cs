using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cro : MonoBehaviour
{
    public AudioSource hi;
    
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && !hi.isPlaying)
        {
            hi.Play();
        }
    }
}

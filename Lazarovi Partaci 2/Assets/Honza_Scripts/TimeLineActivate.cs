using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class TimeLineActivate : MonoBehaviour
{
    public PlayableDirector directorBridge;
    public GameObject marker;
    private void OnTriggerStay(Collider other)
    {

        marker.SetActive(true);
        if(Input.GetKey(KeyCode.E))
        {
            directorBridge.Play();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        marker.SetActive(false);
    }
}

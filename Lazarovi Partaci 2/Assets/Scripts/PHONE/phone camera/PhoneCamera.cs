using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCamera : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Return) && other.tag == "Friendly")
        {
            Debug.Log("Friendly je v triggru a foti se");
            if (other.gameObject.GetComponent<AudioSource>() == true)
            {
                other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
}

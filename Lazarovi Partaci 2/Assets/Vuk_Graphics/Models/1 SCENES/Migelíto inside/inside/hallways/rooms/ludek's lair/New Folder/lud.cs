using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lud : MonoBehaviour
{
    public AudioSource ludek1;
    public AudioSource ludek2;
    public AudioSource ludek3;
    public AudioSource ludek4;

    int i = 0;

    void Start()
    {
        i = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && !ludek1.isPlaying && !ludek2.isPlaying && !ludek3.isPlaying && !ludek4.isPlaying)
        {
            if (i == 0)
            {
                ludek1.Play();
                i++;
            }
            else if (i == 1)
            {
                ludek2.Play();
                i++;
            }
            else if (i == 2)
            {
                ludek3.Play();
                i++;
            }
            else
            {
                ludek4.Play();
                i = 0;
            }
        }
    }
}

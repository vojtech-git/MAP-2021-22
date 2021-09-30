using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnim;
    void OnTriggerEnter(Collider other)
    {
        doorAnim.SetTrigger("OpenClose");
    }
    void OnTriggerExit(Collider other)
    {
        doorAnim.SetTrigger("OpenClose");
    }
}

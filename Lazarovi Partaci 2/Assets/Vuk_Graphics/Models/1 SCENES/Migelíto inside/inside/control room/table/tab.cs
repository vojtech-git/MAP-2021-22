using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tab : MonoBehaviour
{
    public GameObject hol;
    bool bul = false;

    private void Start()
    {
        hol.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hol.SetActive(!bul);
            bul = !bul;
        }
    }
}

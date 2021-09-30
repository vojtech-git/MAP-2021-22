using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGain : MonoBehaviour
{
    [Header("Gain")]
    public int gainHealth = 0;
    void OnTriggerEnter(Collider other)
    {
        GameObject hrac = GameObject.FindGameObjectWithTag("Player");
        Player a = hrac.GetComponent<Player>();
        if (other.gameObject.tag == "Player")
        {
            if(a.currentHealth == a.maxHealth)
            {

            }
            if(a.currentHealth < a.maxHealth)
            {
            a.UseHealth(-gainHealth);
            DestroyObject();
            }
        }
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}

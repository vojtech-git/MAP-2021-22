using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyGain : MonoBehaviour
{
    [Header("Gain")]
    public int gainMoney = 0;
    void OnTriggerEnter(Collider other)
    {
        GameObject hrac = GameObject.FindGameObjectWithTag("Player");
        Player a = hrac.GetComponent<Player>();
        if (other.gameObject.tag == "Player")
        {
            a.UseMoney(-gainMoney);
            DestroyObject();
        }
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public float healValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player.Health != player.MaxHealth)
            {
                player.AddHealth(healValue);
                Destroy(this.gameObject);
            }
        }
    }
}

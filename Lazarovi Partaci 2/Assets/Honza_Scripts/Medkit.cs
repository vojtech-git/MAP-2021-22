using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Pickable
{
    public float healValue;

    protected override void Pickup(Player player)
    {
        if (player.Health != player.MaxHealth)
        {
            player.AddHealth(healValue);
            Destroy(this.gameObject);
        }
    }
}

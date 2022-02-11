using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPickable : Pickable
{
    public int moneyGainAmount = 0;

    protected override void Pickup(Player player)
    {
        player.UseMoney(-moneyGainAmount);
        Destroy(gameObject);
    }
}

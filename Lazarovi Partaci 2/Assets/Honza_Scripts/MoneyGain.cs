using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyGain : Pickable
{
    public int gainMoney = 0;

    protected override void Pickup(Player player)
    {
        player.UseMoney(-gainMoney);
        Destroy(gameObject);
    }
}

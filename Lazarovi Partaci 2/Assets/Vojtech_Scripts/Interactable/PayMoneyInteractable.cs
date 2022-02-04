using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayMoneyInteractable : Interactable
{
    Player player;
    public int cost;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public override void Interact()
    {
        if (player.UseMoney(cost))
        {
            base.Interact();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayMoneyInteractable : QuestInteractable
{
    [Header("Pay Money")]
    public int cost;

    Player player;

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

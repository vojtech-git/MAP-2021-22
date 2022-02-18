using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayMoneyInteractable : QuestInteractable
{
    [Header("Pay Money")]
    public int cost;


    Player player;
    bool paid = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public override void Interact()
    {
        if (player.UseMoney(cost) && !paid)
        {
            base.Interact();
            paid = true;
            Debug.Log("playing to " + gameObject.name);
        }
        else
        {
            Debug.LogWarning("player doesent have enoguh money for " + gameObject.name);
        }
    }
}

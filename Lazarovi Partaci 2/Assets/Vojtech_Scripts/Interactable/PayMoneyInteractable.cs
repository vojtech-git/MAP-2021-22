using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class PayMoneyInteractable : QuestInteractable
{
    [Header("Pay Money")]
    public int cost;
    public AudioSource moneyPaidAllreadySound;
    public AudioSource notEnoughMoneySound;

    Player player;
    bool paid = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        interactionMessage = interactionMessage + " " + cost.ToString();
    }

    public override void Interact()
    {
        // hrac zaplatil
        if (paid)
        {
            moneyPaidAllreadySound.Play();
        }
        // pokud spravne strhne hracovi penize
        else if (player.UseMoney(cost))
        {
            QuestingManager.OnPointGained(GoalType.Interact, itemId);
            paid = true;

            Debug.Log("playing to " + gameObject.name);
        }
        else
        {
            notEnoughMoneySound.Play();
        }
    }
}

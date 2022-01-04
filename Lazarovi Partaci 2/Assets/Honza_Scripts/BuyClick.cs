using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyClick : MonoBehaviour
{
    [Header("Spend")]
    public int spendMoney = 0;
    public Text b;
    public Player player;
    public void Buy()
    {
        if (player.currentMoney >= spendMoney)
        {
            player.UseMoney(spendMoney);
            b.text = spendMoney.ToString() + " ¤";
            DestroyObject();
        }
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}

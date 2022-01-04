using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyShow : MonoBehaviour
{
    public Player player;
    public Text a;
    void Update()
    {
        a.text = player.currentMoney.ToString() + " ¤";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class phoneTime : MonoBehaviour
{
    public TMP_Text text;
    private int hour;
    private int minute;

    // Update is called once per frame
    void Update()
    {
        hour = System.DateTime.Now.Hour;
        minute = System.DateTime.Now.Minute;
        if (minute < 10)
            text.text = "" + hour + ":" + "0" + minute;
        else
            text.text = "" + hour + ":" + minute;
    }
}

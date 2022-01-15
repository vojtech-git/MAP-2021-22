using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatShowInt : MonoBehaviour
{
    public GunScript gunScriptik;
    public Text statUI;
    private void Update()
    {
        statUI.text = gunScriptik.damage.ToString();
    }
}

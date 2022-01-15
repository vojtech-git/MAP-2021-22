using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatShowFloat : MonoBehaviour
{
    // Start is called before the first frame update
    public GunScript gunScriptik;
    public Text statUI;
    public int a;
    private void Update()
    {
        if(a == 1)
        {
            statUI.text = gunScriptik.spread.ToString();
        }
        if (a == 2)
        {
            statUI.text = gunScriptik.magazineSize.ToString();
        }
        if (a == 3)
        {
            statUI.text = gunScriptik.reloadTime.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTestTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!WeaponsCanvas.Instance.menuOpen)
            {
                WeaponsCanvas.Instance.OpenMenu();
            }
            else
            {
                WeaponsCanvas.Instance.CloseMenu();
            }
        }
    }
}

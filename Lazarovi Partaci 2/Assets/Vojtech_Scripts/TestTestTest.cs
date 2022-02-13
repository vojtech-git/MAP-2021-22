using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTestTest : MonoBehaviour
{
    bool weaponMenuActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (weaponMenuActive)
            {
                WeaponsCanvas.Instance.WeaponsMenu.SetActive(false); 
            }
            else
            {
                WeaponsCanvas.Instance.WeaponsMenu.SetActive(true);
            }

            weaponMenuActive = !weaponMenuActive;
        }
    }
}

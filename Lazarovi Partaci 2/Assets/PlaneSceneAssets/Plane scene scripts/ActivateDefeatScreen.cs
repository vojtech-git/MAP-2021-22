using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateDefeatScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public PlaneHealth planehealth;
    public GameObject defeatScreen; 
    public Image warning;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(planehealth.health<=0){
           Invoke("YouAreDead", 1.5f);
        }
    }
      public void YouAreDead()
    {
        
            warning.enabled=false;
            Cursor.lockState = CursorLockMode.None; //defeat screen
             DefeatScreen.jsiDead=true;
             //Time.timeScale = 0f;
            // Debug.Log(Time.timeScale);
             defeatScreen.SetActive(true);
             Cursor.lockState = CursorLockMode.None;
             Cursor.visible=true;
    }
}

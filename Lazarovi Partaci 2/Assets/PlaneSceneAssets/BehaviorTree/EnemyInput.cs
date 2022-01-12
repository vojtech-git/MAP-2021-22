using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour, IController
{   
    public event InputEventFloat ForwardEvent;
    public event InputEventVector TurnEvent;
    public event InputEvent FireEvent;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getKeyboardInput();
    }

    void getKeyboardInput(){

        if(ForwardEvent != null)
        {
            if(Input.GetAxis("Vertical1") !=0) ForwardEvent (Input.GetAxis("Vertical1"));
        }


        if(FireEvent != null)
        {
            FireEvent();
        }
    }
}

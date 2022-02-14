using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource hoverSound;
    public AudioSource clickSound;
    public AudioSource checkBoxSound;
  public void HoverSound(){
        hoverSound.Play();
    }
     public void ClickSound(){
    
        clickSound.Play();
    }

    public void CheckSound(){
    
        checkBoxSound.Play();
    }
}

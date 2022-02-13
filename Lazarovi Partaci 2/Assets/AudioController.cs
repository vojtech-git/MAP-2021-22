using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource hoverSound;
    public AudioSource clickSound;
  public void HoverSound(){
        hoverSound.Play();
    }
     public void ClickSound(){
    
        clickSound.Play();
    }
}

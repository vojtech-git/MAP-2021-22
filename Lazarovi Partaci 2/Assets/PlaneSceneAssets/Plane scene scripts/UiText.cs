using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiText : MonoBehaviour
{
   // [SerializeField] private textWriter textWriter;
    public  Text messageText;
    public AudioSource typingAudioSource;
    public static UiText instance;
   private void Awake(){
    // Application.targetFrameRate =3;
   }
   public void hello()
    {   
      StartTalkingSound();
        textWriter.addWriter_Static(messageText, "Tvé dýchání bylo velmi jednoduše přessssrušeno", .1f,true,StopTalkingSound);
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }


    private void StopTalkingSound(){
        typingAudioSource.Stop();
    }
     private void StartTalkingSound(){
        typingAudioSource.Play();
    }
}

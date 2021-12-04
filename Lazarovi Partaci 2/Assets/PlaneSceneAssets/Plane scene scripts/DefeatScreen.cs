using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatScreen : MonoBehaviour
{
     public static bool jsiDead = false;
    public GameObject defeat;
    public Text messageText;
    public Text messageText2;
    public AudioSource typingAudioSource;

    public Image redhealth;
    public Image greenhealth;
    public Image border;
    public Text healthValue;
    public Image warning;
   
    public static DefeatScreen instance;
    void Start()
    {
       //Cursor.lockState = CursorLockMode.None;
       //JE POTREBA EDIT V PAUSE MENU (PODMINKA) V SHOOTINGU A WEAPON SWITCHINGU(PODMINKY)
       // podminka v metode load player, podminka v pause menu, ve weapon wheelu, weapon switchingu, shootingu, a radek kodu v main menu scriptu
        StartTalkingSound();
       textWriter.addWriter_Static(messageText, "Tvé dýchání bylo velmi jednoduše přerušeno", .1f,true,StopTalkingSound);
     
        StartCoroutine(Text());
      //  Invoke("typeText",2f);
        
      
    }

    IEnumerator Text()  //  <-  its a standalone method
{
	
    yield return new WaitForSecondsRealtime(5);
    StartTalkingSound();
     textWriter.addWriter_Static(messageText2,"Time of elimination: " + System.DateTime.Now.ToString(), .1f,true,StopTalkingSound);
}

  
    // Update is called once per framea
    void Update()
    {
      if (jsiDead==true){
              pepa();
          }
          else{
              josef();
          }
         
    
    }

     public void pepa(){

       
        
          Time.timeScale=0f;
          greenhealth.enabled=false;
          redhealth.enabled=false;
          healthValue.enabled=false;
          border.enabled=false;

          
     }
    public void josef(){

         Time.timeScale=1f;
         warning.enabled=true;
     }
    public void restartGame()
    {
        
       Debug.Log("restart");
       
        SceneManager.LoadScene("plane system");
        defeat.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        jsiDead=false;
        Time.timeScale=1f;
        Debug.Log(Time.timeScale);
        //Debug.Log(jsiDead);
      
  
       
    }

    private void StopTalkingSound(){
        typingAudioSource.Stop();
    }
     private void StartTalkingSound(){
        typingAudioSource.Play();
    }

    public void exitToMenu() 
    {
         Time.timeScale=1f;
            SceneManager.LoadScene("Menu");
          jsiDead = false;
            Cursor.lockState = CursorLockMode.None;
           Time.timeScale=1f;
           Debug.Log(Time.timeScale);
    }
     
 
}

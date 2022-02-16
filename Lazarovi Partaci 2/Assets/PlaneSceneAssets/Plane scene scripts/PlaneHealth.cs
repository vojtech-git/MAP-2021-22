using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlaneHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 100;
    public float collisionDamage;
    [SerializeField] private float maxhealth;
    [SerializeField] private Image healthImage;
    public float healthInPercentage;
    public Text healthPercentage;
    public Image warning;

    public GameObject defeatScreen;

    public GameObject explosionEffect;
    public MeshRenderer planeRenderer; //kvuli explozi
     public float interval = 0.5f;
     public float startDelay = 0f;
     public bool currentState = true;
     public bool defaultState = true;
     bool isBlinking = false;
     public AudioSource collisionSound;
     
    void Start()
    {
        warning.enabled = isBlinking;
    }

    // Update is called once per frame
    void Update()
    { //czechpoint
        if (health <= 0){
           // SceneManager.LoadScene("plane system");
           Instantiate(explosionEffect, transform.position, transform.rotation); //explosion effect
           //planeRenderer.enabled=false; //zmizeni letadla po smrti, destroy by nam nepomohlo, protoze by to znicilo i skripty
            Destroy(this.gameObject);
           // Invoke("YouAreDead", 2f); //abychom videli explosion effect
        }

          if (Input.GetKeyDown(KeyCode.C))
        {
            health = health-20;
            updateHealth();
            Debug.Log(health);
        }

        if(health <=30 && DefeatScreen.jsiDead==false && health>0){
            StartBlink();
        }

        updateHealth();
    }


   /*  public void YouAreDead()
    {
        
           warning.enabled=false;
            Cursor.lockState = CursorLockMode.None; //defeat screen
             DefeatScreen.jsiDead=true;
             //Time.timeScale = 0f;
            // Debug.Log(Time.timeScale);
             defeatScreen.SetActive(true);
             Cursor.lockState = CursorLockMode.None;
             Cursor.visible=true;
    } */

    void OnCollisionEnter(Collision col){ //zvuk
           health -= collisionDamage * col.relativeVelocity.magnitude ; 
            collisionSound.Play();
        
           updateHealth();
           //Debug.Log(health);
    }

    private void  updateHealth(){

        healthImage.fillAmount = health / maxhealth;
        healthInPercentage = (health / maxhealth) * 100;
        healthInPercentage = Mathf.Round(healthInPercentage * 100.0f) * 0.01f;
        healthPercentage.text = healthInPercentage.ToString() + " %";
    }

    public void StartBlink()
     {
        
         if (isBlinking)
             return;
 
         if (warning !=null)
         {
             isBlinking = true;
             InvokeRepeating("ToggleState", startDelay, interval);
         }
     }

      public void ToggleState()
     {
         warning.enabled = !warning.enabled;
 
      /*    // plays the clip at (0,0,0)
         if (clip)
             AudioSource.PlayClipAtPoint(clip,Vector3.zero); */
     }
}

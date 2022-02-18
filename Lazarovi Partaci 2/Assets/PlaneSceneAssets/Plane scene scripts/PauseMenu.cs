using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
   public static bool GameIsPaused = false;  //checkuje zda bezi nejaky skript
    public GameObject pauseMenuUI;
    public bool fpsPlayer;
    [SerializeField] Texture2D crosshair;

    [SerializeField] GameObject redHealth;
    [SerializeField] GameObject greenHealth;
    [SerializeField] GameObject border;
    [SerializeField] GameObject health;

    public AudioSource openSound;
    void Start() {
        pauseMenuUI.SetActive(false);  // pri startu je vyply pause
        GameIsPaused = false; // FIX BUGU, kdyz dame main menu a pak dame new game tak muzeme normalne hrat, jinak jsme nemohli strilet. FIXED
        DefeatScreen.jsiDead=false;
    }
    void Update()
    {
        if (DefeatScreen.jsiDead == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                    openSound.Play();
                }
            }
        } 



        if (PauseMenu.GameIsPaused == true)
        {
            Cursor.lockState = CursorLockMode.None;    //abychom mohli klikat na buttony ktere jsou na pause menu
        }

        /*   else if(weaponWheel.instance !=null){
              if(weaponWheel.instance.WheelEnabled)
              {
                  return;
              }
          } */
        //if (PauseMenu.GameIsPaused == false && defeatscreen.jsiDead==false) Cursor.lockState = CursorLockMode.Locked;

       // if (weaponWheel.instance.WheelEnabled && weaponWheel.instance != null) 
      //  {  
            //aby se nelockovala mys kdyz dame weapon wheel.
          //  Cursor.lockState = CursorLockMode.None;
       // }
      //   }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //pomoci tohoto muzeme delat i slowmotion
        GameIsPaused = false;
        if (crosshair != null)
        {
            Vector2 cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);
            Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);
        }

        if(redHealth != null && greenHealth != null && border != null && health != null ){
        redHealth.SetActive(true);
        greenHealth.SetActive(true);
        border.SetActive(true);
        health.SetActive(true);
        }
        if (fpsPlayer)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }


    void Pause()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //pomoci tohoto muzeme delat i slowmotion
        GameIsPaused = true;
        Cursor.visible = true;  // moznost klikani na buttony, mys nam nezmizi
        Cursor.lockState = CursorLockMode.None; //
        if(redHealth != null && greenHealth != null && border != null && health != null ){
        redHealth.SetActive(false);
        greenHealth.SetActive(false);
        border.SetActive(false);
        health.SetActive(false);
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
         SceneManager.LoadScene("MainMenu");
             // Cursor.lockState = CursorLockMode.None;
             // Cursor.visible=true;
     
     
       
    }

    public void QuitGame()
    {
        Debug.Log("jdu pryc, tohle me nebavi");
        Application.Quit();
    }
}

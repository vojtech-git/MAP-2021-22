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

    [SerializeField] Image redhealth;
    [SerializeField] Image greenhealth;
    [SerializeField] Image border;
    [SerializeField] Text healthValue;
    [SerializeField] Image warning;
    public Texture2D crosshair;

    public static DefeatScreen instance;
    void Start()
    {
        //Cursor.lockState = CursorLockMode.None;
        //JE POTREBA EDIT V PAUSE MENU (PODMINKA) V SHOOTINGU A WEAPON SWITCHINGU(PODMINKY)
        // podminka v metode load player, podminka v pause menu, ve weapon wheelu, weapon switchingu, shootingu, a radek kodu v main menu scriptu
        StartTalkingSound();
        textWriter.addWriter_Static(messageText, "Your breathing has been easily stopped", .1f, true, StopTalkingSound);

        StartCoroutine(Text());
        //  Invoke("typeText",2f);
    }

    IEnumerator Text()  //  <-  its a standalone method
    {
        yield return new WaitForSecondsRealtime(5);
        StartTalkingSound();
        textWriter.addWriter_Static(messageText2, "Time of elimination: " + System.DateTime.Now.ToString(), .1f, true, StopTalkingSound);
    }


    // Update is called once per framea
    void Update()
    {
        if (jsiDead == true)
        {
            pepa();
        }
        else
        {
            josef();
        }
    }

    public void pepa()
    {
        Time.timeScale = 0f;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        if (redhealth != null && greenhealth != null && border != null && healthValue != null)
        {
            greenhealth.enabled = false;
            redhealth.enabled = false;
            healthValue.enabled = false;
            border.enabled = false;
        }
    }
    public void josef()
    {
        Time.timeScale = 1f;
        if (warning != null)
        {
            warning.enabled = true;
        }
    }
    public void RestartGame()
    {
        Debug.Log("restart");

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Vector2 cursorOffset = new Vector2(crosshair.width/2, crosshair.height/2);
        //Cursor.SetCursor(crosshair, cursorOffset,CursorMode.Auto);
        //defeat.SetActive(false);
        //Cursor.lockState = CursorLockMode.None;
        //jsiDead=false;
        //Time.timeScale=1f;
        //Debug.Log(Time.timeScale);
        ////Debug.Log(jsiDead);


        SceneStateManager.Instance.RestartGame();
    }

    private void StopTalkingSound()
    {
        typingAudioSource.Stop();
    }
    private void StartTalkingSound()
    {
        typingAudioSource.Play();
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        jsiDead = false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        Debug.Log(Time.timeScale);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}

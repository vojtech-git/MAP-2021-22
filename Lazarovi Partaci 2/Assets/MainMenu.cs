using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  //public AudioSource iHateYou;

    public void PlayGame () {
        Destroy(GameObject.Find("Music Player"));

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Time.timeScale=1f; //defeat screen
          //PauseMenu.GameIsPaused=false; // fix bugu weapon wheelu
    }

    public void QuitGame() {
       // iHateYou.Play();

        //Debug.Log("Quit");

        Application.Quit();
    }
}

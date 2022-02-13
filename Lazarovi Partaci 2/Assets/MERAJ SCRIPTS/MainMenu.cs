using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public AudioSource iHateYou;

    private void Start()
    {

    }

    public void PlayGame () {
        Destroy(GameObject.Find("Music Player"));

        // nastavi start of game = true, naète prvni scénu
        SceneStateManager.Instance.RestartGame();

        Time.timeScale=1f; //defeat screen
          //PauseMenu.GameIsPaused=false; // fix bugu weapon wheelu
    }

    public void QuitGame() {
        // iHateYou.Play();

        //Debug.Log("Quit");

        QuestCanvas.Instance.ClearUI();

        Application.Quit();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{
    public QuestlineSO storyQuestline;
    static bool startOfGame = true;

    private static SceneStateManager instance;
    public static SceneStateManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (startOfGame)
            {
                //Debug.Log("starting the game");

                startOfGame = false;
                QuestCanvas.Instance.ClearUI();
                SaveData.WipeSaveData();

                QuestingManager.StartQuestline(storyQuestline.questline);
            }
        }
    }

    public void RestartGame()
    {
        startOfGame = true;
        SceneManager.LoadScene("System 0");
    }
}



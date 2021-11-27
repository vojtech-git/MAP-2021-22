using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneWithButton : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Destroy(GameObject.Find("Push It To The Limit")); // hudba prestane hrat po nacteni hernich scen - Vuk 7.11.

        SceneManager.LoadScene(sceneName);
    }
}

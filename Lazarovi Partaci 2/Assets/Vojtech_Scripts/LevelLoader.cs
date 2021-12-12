using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{    
    public Animator transition;
    public float transitionTime;
    public int targetSceneIndex;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadNextScene(targetSceneIndex);
        }
    }

    public void LoadNextScene(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        if (levelIndex == 10)
        {
            GameStateManager.Instance.FPS?.SetActive(false);
        }
        else
        {
            GameStateManager.Instance.FPS?.SetActive(true);
        }

        SceneManager.LoadScene(levelIndex);
    }
}

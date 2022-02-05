using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public bool instant;

    public Animator transition;
    public float transitionTime;
    public string targetSceneName;
    public Vector3 targetPosition;



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerFaction"))
        {
            if (instant)
            {
                LoadNextScene(targetSceneName);
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                LoadNextScene(targetSceneName);
            }
        }
    }

    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        SaveData.loadPosition = targetPosition;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}

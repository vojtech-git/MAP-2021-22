using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titulky : MonoBehaviour
{
    public AudioSource audio;

    private void Start()
    {
        StartCoroutine(EndOfLazaroviPartaci2());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
        
    }

    IEnumerator EndOfLazaroviPartaci2()
    {
        yield return new WaitForSecondsRealtime(audio.clip.length + 1f);

        SceneManager.LoadSceneAsync("Main Menu");
    }
}

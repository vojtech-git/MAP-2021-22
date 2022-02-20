using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class introscene : MonoBehaviour
{
    public VideoPlayer IntroVideo;
    public float time;
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(WhenTheImpostorIsSus());
    }

    IEnumerator WhenTheImpostorIsSus()
    {
        yield return new WaitForSecondsRealtime(time);

        IntroVideo.Play();

        yield return new WaitForSecondsRealtime((float)IntroVideo.length + 2f);

        SceneManager.LoadSceneAsync("System 0");
    }
}

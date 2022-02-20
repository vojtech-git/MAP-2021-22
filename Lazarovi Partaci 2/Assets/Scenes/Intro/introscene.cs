using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class introscene : MonoBehaviour
{
    public VideoPlayer IntroVideo;
    public float time;

    //public GameObject mainCam;
    //public GameObject cam2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WhenTheImpostorIsSus());
    }

    IEnumerator WhenTheImpostorIsSus()
    {
        yield return new WaitForSecondsRealtime(time);

        IntroVideo.Play();

        //yield return new WaitForSecondsRealtime(66f);

        //mainCam.SetActive(false);

        //cam2.SetActive(true);
    }
}

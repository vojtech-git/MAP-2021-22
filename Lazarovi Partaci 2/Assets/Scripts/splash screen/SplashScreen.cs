using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public GameObject v1;
    public GameObject v2;
    public new GameObject audio;

    [Header("Seconds after Reniston logo")]
    public float waitAfterLogo = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        audio.GetComponent<AudioSource>().Play();
        StartCoroutine(enumerator(v1.GetComponent<VideoPlayer>(), v2.GetComponent<VideoPlayer>()));
    }

    IEnumerator enumerator(VideoPlayer r, VideoPlayer lp)
    {
        yield return new WaitForSecondsRealtime(2f);

        r.Play();
        yield return new WaitForSecondsRealtime((float)r.length);
        r.Stop();

        yield return new WaitForSecondsRealtime(waitAfterLogo);
        
        lp.Play();
        yield return new WaitForSecondsRealtime((float)lp.length);
        lp.Stop();

        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadSceneAsync("Plane system");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudioManager : MonoBehaviour
{
    private static SceneAudioManager instance;
    public static SceneAudioManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    public void PlayAudioMethod(AudioSource[] audioSources)
    {
        StartCoroutine(PlayAudio(audioSources));
    }

    public IEnumerator PlayAudio(AudioSource[] audioSources)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] != null)
            {
                if (audioSources[i].isPlaying)
                {
                    audioSources[i].Stop();
                }

                audioSources[i].Play();
                yield return new WaitUntil(() => !audioSources[i].isPlaying); // wait untill musi pøijmout func jako parametr. proto vytvaøím anonym metodu
            }
        }
    }
}

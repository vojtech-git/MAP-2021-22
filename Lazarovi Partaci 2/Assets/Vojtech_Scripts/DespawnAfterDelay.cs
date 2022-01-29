using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnAfterDelay : MonoBehaviour
{
    public float delay;

    private void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(delay);
        Destroy(gameObject);
    }
}

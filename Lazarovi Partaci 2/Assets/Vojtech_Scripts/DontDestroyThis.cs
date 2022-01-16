using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyThis : MonoBehaviour
{
    string objID;

    private void Awake()
    {
        objID = name /*+ transform.position.ToString()*/;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

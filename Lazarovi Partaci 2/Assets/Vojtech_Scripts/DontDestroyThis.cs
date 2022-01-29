using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyThis : MonoBehaviour
{
    string objID;

    public static DontDestroyThis thisInstance;


    private void Awake()
    {
        if (thisInstance == null)
        {
            thisInstance = this;
        }
        else if (thisInstance != null && thisInstance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

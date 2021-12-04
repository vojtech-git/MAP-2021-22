using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyThis : MonoBehaviour
{
    string objID;

    private void Awake()
    {
        objID = name + transform.position.ToString();
    }

    void Start()
    {
        foreach (DontDestroyThis dontDestroyInstance in Object.FindObjectsOfType<DontDestroyThis>())
        {
            if (dontDestroyInstance != this)
            {
                if (dontDestroyInstance.objID == objID)
                {
                    Destroy(gameObject);
                }
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}

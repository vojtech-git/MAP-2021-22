using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCanvas : MonoBehaviour
{
    public Transform QuestVerticalLayout;

    private static QuestCanvas instance;
    public static QuestCanvas Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ClearUI()
    {
        foreach (Transform item in QuestVerticalLayout)
        {
            Destroy(item.gameObject);
        }
    }
}

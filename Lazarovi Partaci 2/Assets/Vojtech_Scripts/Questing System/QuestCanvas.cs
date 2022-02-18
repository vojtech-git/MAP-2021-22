using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCanvas : MonoBehaviour
{
    public Transform questVerticalLayout;

    public Text interactionMessageUi;

    [Header("Quest prefabs")]
    public GameObject questTitlePrefab;
    public GameObject goalDescriptionPrefab;
    public GameObject descriptionsLayoutPrefab;

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
        foreach (Transform item in questVerticalLayout)
        {
            Destroy(item.gameObject);
        }
    }
}

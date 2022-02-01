using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryObject : MonoBehaviour
{
    string currentSceneName;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void OnDestroy()
    {
        // pokuid neexistuje dictionary t�to sc�ny tak ho vytvo�
        if (!SaveData.ObjectEnabledStates.ContainsKey(currentSceneName))
        {
            SaveData.ObjectEnabledStates.Add(currentSceneName, new Dictionary<string, bool>());
        }

        // pokud neexistuje log v dictionary t�hle sc�nu o tomhle gameobjectu tak ho vytvo�
        if (!SaveData.ObjectEnabledStates[currentSceneName].ContainsKey(gameObject.name))
        {
            SaveData.ObjectEnabledStates[currentSceneName].Add(gameObject.name, gameObject.activeSelf);
        }
        else
        {
            SaveData.ObjectEnabledStates[currentSceneName][gameObject.name] = gameObject.activeSelf;
        }

        Debug.Log("OnDestory " + gameObject.name + " triggered");
    }

    public void ApplySaveData()
    {
        if (SaveData.ObjectEnabledStates.ContainsKey(currentSceneName) && SaveData.ObjectEnabledStates[currentSceneName].ContainsKey(gameObject.name))
        {
            gameObject.SetActive(SaveData.ObjectEnabledStates[currentSceneName][gameObject.name]);

            Debug.Log("Applied data on " + gameObject.name);
        }
    }
}

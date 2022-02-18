using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryObject : MonoBehaviour
{
    public bool startTurnedOn;
    // IDENTIFIKACE BY MOHLA OBSAHOVAT PARENT NAME

    private void Awake()
    {

    }
    
    private void OnDestroy()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // pokuid neexistuje dictionary této scény tak ho vytvoø
        if (!SaveData.objectEnabledStates.ContainsKey(currentSceneName))
        {
            SaveData.objectEnabledStates.Add(currentSceneName, new Dictionary<string, bool>());

            //Debug.Log("Creating log for a SCENE: " + SceneManager.GetActiveScene().name);
        }

        // pokud neexistuje log v dictionary týhle scénu o tomhle gameobjectu tak ho vytvoø
        if (!SaveData.objectEnabledStates[currentSceneName].ContainsKey(gameObject.name))
        {
            SaveData.objectEnabledStates[currentSceneName].Add(gameObject.name, gameObject.activeSelf);

            //Debug.Log("Creating log for OBJECT: " + gameObject.name);
        }
        // pokud exituje log tak na nìj zapiš
        else
        {
            SaveData.objectEnabledStates[currentSceneName][gameObject.name] = gameObject.activeSelf;

            //Debug.Log("Rewriting data for OBJECT: " + gameObject.name);
        }


        if (!SaveData.objectPositions.ContainsKey(currentSceneName))
        {
            SaveData.objectPositions.Add(currentSceneName, new Dictionary<string, Vector3>());
        }

        if (!SaveData.objectPositions[currentSceneName].ContainsKey(gameObject.name))
        {
            SaveData.objectPositions[currentSceneName].Add(gameObject.name, gameObject.transform.position);
        }
        else
        {
            SaveData.objectPositions[currentSceneName][gameObject.name] = gameObject.transform.position;
        }
    }

    public void TryApplySaveData()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (SaveData.objectEnabledStates.ContainsKey(currentSceneName) && SaveData.objectEnabledStates[currentSceneName].ContainsKey(gameObject.name))
        {
            gameObject.SetActive(SaveData.objectEnabledStates[currentSceneName][gameObject.name]);

            //Debug.Log("Applied enabled data on " + gameObject.name);
        }
        else
        {
            //Debug.Log("No enabled log found for: " + gameObject.name);
        }

        if (SaveData.objectPositions.ContainsKey(currentSceneName) && SaveData.objectPositions[currentSceneName].ContainsKey(gameObject.name))
        {
            transform.position = SaveData.objectPositions[currentSceneName][gameObject.name];
            //Debug.Log("position applyied for + " + gameObject.name + " position: " + SaveData.objectPositions[SceneManager.GetActiveScene().name][gameObject.name]);
        }
        else
        {
            //Debug.Log("No position log found for: " + gameObject.name);
        }
    }
}

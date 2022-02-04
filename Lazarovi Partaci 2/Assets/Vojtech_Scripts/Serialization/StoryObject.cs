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
        // pokuid neexistuje dictionary této scény tak ho vytvoø
        if (!SaveData.objectEnabledStates.ContainsKey(SceneManager.GetActiveScene().name))
        {
            SaveData.objectEnabledStates.Add(SceneManager.GetActiveScene().name, new Dictionary<string, bool>());

            //Debug.Log("Creating log for a SCENE: " + SceneManager.GetActiveScene().name);
        }

        // pokud neexistuje log v dictionary týhle scénu o tomhle gameobjectu tak ho vytvoø
        if (!SaveData.objectEnabledStates[SceneManager.GetActiveScene().name].ContainsKey(gameObject.name))
        {
            SaveData.objectEnabledStates[SceneManager.GetActiveScene().name].Add(gameObject.name, gameObject.activeSelf);

            //Debug.Log("Creating log for OBJECT: " + gameObject.name);
        }
        // pokud exituje log tak na nìj zapiš
        else
        {
            SaveData.objectEnabledStates[SceneManager.GetActiveScene().name][gameObject.name] = gameObject.activeSelf;

            //Debug.Log("Rewriting data for OBJECT: " + gameObject.name);
        }


        if (!SaveData.objectPositions.ContainsKey(SceneManager.GetActiveScene().name))
        {
            SaveData.objectPositions.Add(SceneManager.GetActiveScene().name, new Dictionary<string, Vector3>());
        }

        if (!SaveData.objectPositions[SceneManager.GetActiveScene().name].ContainsKey(gameObject.name))
        {
            SaveData.objectPositions[SceneManager.GetActiveScene().name].Add(gameObject.name, gameObject.transform.position);
        }
        else
        {
            SaveData.objectPositions[SceneManager.GetActiveScene().name][gameObject.name] = gameObject.transform.position;
        }
    }

    public void TryApplySaveData()
    {
        if (SaveData.objectEnabledStates.ContainsKey(SceneManager.GetActiveScene().name) && SaveData.objectEnabledStates[SceneManager.GetActiveScene().name].ContainsKey(gameObject.name))
        {
            gameObject.SetActive(SaveData.objectEnabledStates[SceneManager.GetActiveScene().name][gameObject.name]);


            //Debug.Log("Applied enabled data on " + gameObject.name);
        }
        else
        {
            //Debug.Log("No enabled log found for: " + gameObject.name);
        }

        if (SaveData.objectPositions.ContainsKey(SceneManager.GetActiveScene().name) && SaveData.objectPositions[SceneManager.GetActiveScene().name].ContainsKey(gameObject.name))
        {
            transform.position = SaveData.objectPositions[SceneManager.GetActiveScene().name][gameObject.name];
        }
        else
        {
            //Debug.Log("No position log found for:" + gameObject.name);
        }
    }
}

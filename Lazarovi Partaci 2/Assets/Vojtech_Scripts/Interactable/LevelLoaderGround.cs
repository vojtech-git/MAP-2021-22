using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderGround : Interactable
{
    [Header("Level Loader")]
    public Animator transition;
    public float transitionTime;
    public string targetSceneName;
    public Vector3 targetPosition;

    private void Start()
    {
        interactionMessage = "P�esunout se do sc�ny " + targetSceneName;
    }

    public override void Interact()
    {
        StartCoroutine(LoadLevel(targetSceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        SaveData.loadPosition = targetPosition;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);

        QuestCanvas.Instance.interactionMessageUi.text = "";
    }
}

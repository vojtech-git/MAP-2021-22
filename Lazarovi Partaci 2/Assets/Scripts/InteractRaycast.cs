using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractRaycast : MonoBehaviour
{
    public Transform playerCamera;
    public int interactionDistance = 3;
    public Quest hitQuest;
    public int hitGatherObjectID;

    GameObject acceptQuestText;

    private void Start()
    {
        //playerCamera = Camera.main.transform;

        acceptQuestText = QuestingSystem.uiManager.acceptQuestText.gameObject;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            #region Questing Interaction Logic
            if (hit.transform.CompareTag("quest giver"))
            {
                // zbavit se geComponentu v updatu nejlepsi by bylo najit nejakou metoduc co se spusti jenom kdyz na to najedes onMouseOver??
                hitQuest = hit.transform.GetComponent<QuestGiver>().qGiverQuest;

                // jestli je to questgiver a nemam quest v dictionary zobraz acceptUI
                if (!QuestingSystem.playerCompletedQuests.ContainsKey(hitQuest.title) && !QuestingSystem.activeQuests.ContainsKey(hitQuest.title))
                {
                    acceptQuestText.SetActive(true);
                }

                // jestli je to questgiver, zmacknu na nej "e" a nemam quest S TIMHLE TITLEM v playerCompletedQuests vem si ho
                // (kdyz budou mit questy stejnej title tak to nebude fungovat)
                // !!!(pozdeji upravit na usekey)!!!
                if (Input.GetKeyDown(KeyCode.E) && acceptQuestText.GetComponent<Text>().IsActive())
                {
                    QuestingSystem.AcceptQuest(hitQuest);
                    acceptQuestText.SetActive(false);
                }
            }
            if (!hit.transform.CompareTag("quest giver"))
            {
                acceptQuestText.SetActive(false);
            }
            #endregion

            #region tohle je jen testovaci if na to jestli funguje progresování questem
            if (hit.transform.CompareTag("gather object"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitGatherObjectID = hit.transform.GetComponent<GatherObject>().id;

                    QuestingSystem.ProgressQuests(GoalType.Gather, hitGatherObjectID);

                    hit.transform.gameObject.SetActive(false);
                }
            }
            #endregion

        }
        else
        {
            acceptQuestText.SetActive(false);
        }
    }
}

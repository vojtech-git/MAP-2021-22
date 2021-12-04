using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractRaycast : MonoBehaviour
{
    public Transform playerCamera;
    public int interactionDistance = 3;

    public GameObject acceptQuestText;
    int hitGatherObjectID;
    bool cursorLocked = true;
    GameStateManager gameStateManager;

    private void Start()
    {
        gameStateManager = GameStateManager.Instance;

        acceptQuestText = GameStateManager.Instance.acceptQuestText.gameObject;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            #region Questing Interaction Logic
            if (hit.transform.CompareTag("quest giver"))
            {
                if (!gameStateManager.questSelectionMenu.activeSelf)
                {
                    acceptQuestText.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E) && cursorLocked)
                {
                    QuestingSystem.Instance.OpenQuestMenu(hit.transform.GetComponent<QuestGiver>().qGiverQuests);
                    acceptQuestText.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    cursorLocked = false;
                }
                else if (Input.GetKeyDown(KeyCode.E) && !cursorLocked)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    QuestingSystem.Instance.CloseQuestMenu();
                    cursorLocked = true;
                    Cursor.visible = false;
                }

                #region zastarala logika
                //// zbavit se geComponentu v updatu nejlepsi by bylo najit nejakou metoduc co se spusti jenom kdyz na to najedes onMouseOver??


                //// jestli je to questgiver a nemam quest v dictionary zobraz acceptUI
                //if (!QuestingSystem.playerCompletedQuests.ContainsKey(hitQuest.title) && !QuestingSystem.activeQuests.ContainsKey(hitQuest.title))
                //{
                //    acceptQuestText.SetActive(true);
                //}

                //// jestli je to questgiver, zmacknu na nej "e" a nemam quest S TIMHLE TITLEM v playerCompletedQuests vem si ho
                //// (kdyz budou mit questy stejnej title tak to nebude fungovat)
                //// !!!(pozdeji upravit na usekey)!!!
                //if (Input.GetKeyDown(KeyCode.E) && acceptQuestText.GetComponent<Text>().IsActive())
                //{
                //    QuestingSystem.AcceptQuest(hitQuest);
                //    acceptQuestText.SetActive(false);
                //}
                #endregion
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
                    hit.transform.GetComponent<GatherObject>().ItemPicked();
                }
            }
            #endregion

            #region elevator interakce
            if (hit.transform.CompareTag("elevator"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.GetComponentInParent<ElevatorParent>().moveElevator = true;
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

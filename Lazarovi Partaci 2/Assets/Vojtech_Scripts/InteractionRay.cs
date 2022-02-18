using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRay : MonoBehaviour
{
    public float interactionDistance;
    public LayerMask interactLayer;
    public GameObject mainCamera;

    private bool displayingInteractMsg = false;

    private void Update()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactLayer))
        {
            if (!displayingInteractMsg)
            {
                if (hit.transform.TryGetComponent(out Interactable interactable))
                {
                    QuestCanvas.Instance.interactionMessageUi.text = interactable.interactionMessage;

                    displayingInteractMsg = true;
                } 
            }
        }
        else
        {
            if (displayingInteractMsg)
            {
                QuestCanvas.Instance.interactionMessageUi.text = "";
                displayingInteractMsg = false; 
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out RaycastHit raycastHit, interactionDistance, interactLayer))
            {
                if (raycastHit.transform.TryGetComponent(out Interactable interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }
}

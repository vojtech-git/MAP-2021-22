using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("interactable")]
    public string interactionMessage;

    private void Start()
    {
        gameObject.layer = 17;
    }

    /// <summary>
    /// V interact ray volam tuhle metodu kdyz mirim na tenhle obj a d�m interact button na kl�vesnici.
    /// </summary>
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }
}

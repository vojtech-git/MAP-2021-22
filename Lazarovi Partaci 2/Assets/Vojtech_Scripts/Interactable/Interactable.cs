using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("interactable")]
    public string interactionMessage;

    /// <summary>
    /// V interact ray volam tuhle metodu kdyz mirim na tenhle obj a d�m interact button na kl�vesnici.
    /// </summary>
    public virtual void Interact()
    {
        
    }
}

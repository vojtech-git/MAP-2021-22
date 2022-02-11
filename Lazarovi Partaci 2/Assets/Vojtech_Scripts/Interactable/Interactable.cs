using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("interactable")]
    public string interactionMessage;

    /// <summary>
    /// V interact ray volam tuhle metodu kdyz mirim na tenhle obj a dám interact button na klávesnici.
    /// </summary>
    public virtual void Interact()
    {
        
    }
}

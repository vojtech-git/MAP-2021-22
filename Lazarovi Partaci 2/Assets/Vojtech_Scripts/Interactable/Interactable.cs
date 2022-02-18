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
    /// V interact ray volam tuhle metodu kdyz mirim na tenhle obj a dám interact button na klávesnici.
    /// </summary>
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }
}

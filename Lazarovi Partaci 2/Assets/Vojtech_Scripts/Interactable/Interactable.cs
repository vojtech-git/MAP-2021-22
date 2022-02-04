using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("interactable")]
    public int id;
    public string interactionMessage;

    /// <summary>
    /// V interact ray volam tuhle metodu kdyz mirim na tenhle obj a d�m interact button. Base metoda vol� questing.
    /// </summary>
    public virtual void Interact()
    {
        QuestingManager.OnPointGained(GoalType.Interact, id);
    }
}

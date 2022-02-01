using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestButtonMothership : MonoBehaviour
{
    public int buttonId;
    public void PressButton()
    {
        QuestingManager.OnPointGained(GoalType.PressButton, buttonId);
    }
}

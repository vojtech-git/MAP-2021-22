using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public static List<Questline> activeQuestLines = new List<Questline>();
    public static List<Questline> completedQuestLines = new List<Questline>();
}
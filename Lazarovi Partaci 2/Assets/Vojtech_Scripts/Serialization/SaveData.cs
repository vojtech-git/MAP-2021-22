using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public static List<Questline> activeQuestLines = new List<Questline>();
    public static List<Questline> completedQuestLines = new List<Questline>();

    public static Dictionary<string, Dictionary<string, bool>> ObjectEnabledStates = new Dictionary<string, Dictionary<string, bool>>();

    public static int playerMoney;

    public static Vector3 loadPosition = new Vector3(0, 0, 0);

    public static bool leftSceneMothership1;
}
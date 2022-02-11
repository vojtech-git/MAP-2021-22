using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public static List<Questline> activeQuestLines = new List<Questline>();
    public static List<Questline> completedQuestLines = new List<Questline>();

    public static Dictionary<string, Dictionary<string, bool>> objectEnabledStates = new Dictionary<string, Dictionary<string, bool>>();
    public static Dictionary<string, Dictionary<string, Vector3>> objectPositions = new Dictionary<string, Dictionary<string, Vector3>>();

    public static int playerMoney;

    public static Vector3 loadPosition = new Vector3(0, 0, 0);

    public static bool leftSceneMothership1;

    public static void WipeSaveData()
    {
        activeQuestLines = new List<Questline>();
        completedQuestLines = new List<Questline>();
        objectEnabledStates = new Dictionary<string, Dictionary<string, bool>>();
        objectPositions = new Dictionary<string, Dictionary<string, Vector3>>();
        playerMoney = 0;
        loadPosition = new Vector3(0, 0, 0);
        leftSceneMothership1 = false;
    }
}
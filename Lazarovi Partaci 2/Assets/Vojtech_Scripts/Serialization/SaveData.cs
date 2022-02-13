using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public static List<Questline> activeQuestLines = new List<Questline>();
    public static List<Questline> completedQuestLines = new List<Questline>();

    // story objects
    public static Dictionary<string, Dictionary<string, bool>> objectEnabledStates = new Dictionary<string, Dictionary<string, bool>>();
    public static Dictionary<string, Dictionary<string, Vector3>> objectPositions = new Dictionary<string, Dictionary<string, Vector3>>();

    // player
    public static int playerMoney;
    public static Vector3 loadPosition = new Vector3(0, 0, 0);
    public static Dictionary<string, List<WeaponMod>> weaponMods = new Dictionary<string, List<WeaponMod>>();

    public static bool leftSceneMothership1;

    public static void WipeSaveData()
    {
        // wipuju savedata v merajove scriptu kdyz odejdes z hry

        activeQuestLines = new List<Questline>();
        completedQuestLines = new List<Questline>();
        objectEnabledStates = new Dictionary<string, Dictionary<string, bool>>();
        objectPositions = new Dictionary<string, Dictionary<string, Vector3>>();
        playerMoney = 0;
        loadPosition = new Vector3(0, 0, 0);
        leftSceneMothership1 = false;
    }
}
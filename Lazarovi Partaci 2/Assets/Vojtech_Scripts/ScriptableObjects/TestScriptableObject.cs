using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Questline", menuName = "ScriptableObjects/Questline")]
public class TestScriptableObject : ScriptableObject
{
    public Quest[] quests;
}

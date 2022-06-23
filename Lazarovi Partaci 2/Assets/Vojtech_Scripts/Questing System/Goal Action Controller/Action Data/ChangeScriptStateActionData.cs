using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangeScriptStateActionData
{
    public bool finalState;
    public MonoBehaviour[] componentsToAffect;
}

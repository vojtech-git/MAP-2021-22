using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangeStateStructure
{
    [SerializeField]
    public GameObject objectToChange;
    [SerializeField]
    public bool targetState;
}

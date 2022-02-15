using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponMod
{
    [Header("Mod stats")]
    public string name;
    public WeaponModType type;
    public int cost;

    [Header("Graphics")]
    public GameObject model;
    public GameObject ui;
}

public enum WeaponModType
{
    muzzle, scope, magazine, special
}
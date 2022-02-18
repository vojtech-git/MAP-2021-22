using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponMod
{
    [Header("Specifications")]
    public string name;
    public WeaponModType type;
    public int cost;

    [Header("Stats")]
    public int damage;
    public float timeBetweenShooting;
    public float spread;
    public float range;
    public float reloadTime;
    public float timeBetweenShots;
    public int magazineSize;
    public int bulletsPerTap;
    public int bulletsMags;
    public bool allowButtonHold;

    [Header("Graphics")]
    public GameObject model;
    public GameObject ui;
}

public enum WeaponModType
{
    muzzle, scope, magazine, special
}
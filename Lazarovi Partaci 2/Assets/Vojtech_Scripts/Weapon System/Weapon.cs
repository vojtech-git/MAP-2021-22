using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public WeaponMod[] muzzleMods;
    public WeaponMod[] scopeMods;
    public WeaponMod[] magazineMods;
    public WeaponMod[] specialMods;

    [HideInInspector] public List<WeaponMod> unlockedMods = new List<WeaponMod>();
    public WeaponMod[] equipedMods { get; private set; }
}

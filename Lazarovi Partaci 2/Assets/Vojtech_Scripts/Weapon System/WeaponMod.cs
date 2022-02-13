using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponMod", menuName = "Weapons/Mod")]
public class WeaponMod : ScriptableObject
{
    public string ModName;
    public WeaponModType weaponModType;
    public GameObject modModel;

    public void EquipMod(Weapon weaponToEquipTo)
    {
        Debug.Log("Equiping mod " + name + " into the slot " + (int)weaponModType + " to the " + weaponToEquipTo.weaponName);

        weaponToEquipTo.weaponMods[(int)weaponModType] = this;
    }

    public void ApplyGraphics()
    {
        // nastavi ingame vizualni reprezentaci na modelu zbrane a ve weaponWheelu

        // weapon wheel bude asi painfull protoze nepredpokalam ze m� rozdelenou grafiku podle jednotlivejch mod�
        // ale weapon model by mel byt vpohode. proste vymaze ten mod co je tam ted a hodi tam tento
    }
}

public enum WeaponModType
{
    muzzle, scope, magazine, special
}
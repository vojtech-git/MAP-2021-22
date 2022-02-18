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

    public List<WeaponMod> boughtMods;
    public WeaponMod[] equippedMods;
    public SkinnedMeshRenderer[] currentMeshes;
    List<WeaponMod[]> allWeaponMods;

    private void OnEnable()
    {
        allWeaponMods = new List<WeaponMod[]>();
        int numOfSlots = System.Enum.GetNames(typeof(WeaponModType)).Length;
        equippedMods = new WeaponMod[numOfSlots];
        currentMeshes = new SkinnedMeshRenderer[numOfSlots];
        boughtMods = new List<WeaponMod>();        
    }

    /// <summary>
    /// Vybaví mod.
    /// </summary>
    /// <param name="modToEquip"></param>
    public void EquipMod(WeaponMod modToEquip)
    {
        //Debug.Log("Equiping mod " + modToEquip.name + " on " + weaponName);

        equippedMods[(int)modToEquip.type] = modToEquip;

        WeaponManager.OnModEquipped(this, modToEquip);
    }
    /// <summary>
    /// Koupí a automaticky vybaví mod.
    /// </summary>
    /// <param name="modToBuy"></param>
    public void BuyMod(WeaponMod modToBuy)
    {
        if (!boughtMods.Contains(modToBuy) /* && jeslti staci penize */)
        {
            //Debug.Log("Buying mod " + modToBuy.name + " for " + weaponName);

            boughtMods.Add(modToBuy);
            EquipMod(modToBuy);
        }
    }
}

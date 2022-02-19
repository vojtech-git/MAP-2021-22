using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Weapon Specifications")]
    public string weaponName;

    public WeaponMod[] muzzleMods;
    public WeaponMod[] scopeMods;
    public WeaponMod[] magazineMods;
    public WeaponMod[] specialMods;

    [Header("Current")]
    public List<WeaponMod> boughtMods;
    public WeaponMod[] equippedMods;

    private void OnEnable()
    {
        equippedMods = new WeaponMod[4];
        boughtMods = new List<WeaponMod>();
    }

    /// <summary>
    /// Vybav� mod.
    /// </summary>
    /// <param name="modToEquip"></param>
    public void EquipMod(WeaponMod modToEquip)
    {
        //Debug.Log("Equiping mod " + modToEquip.name + " on " + weaponName);

        equippedMods[(int)modToEquip.type] = modToEquip;

        WeaponManager.OnModEquipped(this, modToEquip);
    }
    /// <summary>
    /// Koup� a automaticky vybav� mod.
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

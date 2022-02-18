using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class WeaponManager
{
    public static Action<Weapon, WeaponMod> onModEquipped;

    public static void OnModEquipped(Weapon weaponSender, WeaponMod equippedMod)
    {
        onModEquipped?.Invoke(weaponSender, equippedMod);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponModButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public WeaponMod mod;

    private Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Weapon selectedWeapon = WeaponsCanvas.Instance.selectedWeapon;

        if (selectedWeapon.boughtMods.Contains(mod))
        {
            selectedWeapon.EquipMod(mod);
        }
        else
        {
            selectedWeapon.BuyMod(mod);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.text = "hovering " + mod.name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = mod.name;
    }

    public void BuyMod()
    {
        Debug.Log("Buying mod " + mod.name);
    }

    public void ShowLockedMessage()
    {
        Debug.Log(mod.name + " mod locked");
    }
}

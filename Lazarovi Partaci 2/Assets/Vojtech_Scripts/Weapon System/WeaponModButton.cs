using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponModButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;

    WeaponMod mod;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked mod button " + mod.weaponModName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.text = "hovering";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = "exit";
    }

    public void BuyMod()
    {
        Debug.Log("Buying mod " + mod.weaponModName);        
    }

    public void ShowLockedMessage()
    {
        Debug.Log(mod.weaponModName + " mod locked");
    }


}

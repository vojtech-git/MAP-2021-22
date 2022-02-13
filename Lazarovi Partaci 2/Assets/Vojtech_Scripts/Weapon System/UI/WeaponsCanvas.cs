using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsCanvas : MonoBehaviour
{
    [Header("Menus")]
    public GameObject WeaponsMenu;
    public GameObject SpecificWeaponMenu;
    public GameObject specificModMenu;
    [Header("Titles")]
    public Text specificWeaponTitle;
    public Text specifiModTitle;
    [Header("Mod vertical layout")]
    public VerticalLayoutGroup modVerticalLayout;
    [Header("Prefab")]
    public GameObject ModButtonPrefab;

    [HideInInspector] public Weapon clickedWeapon;

    private static WeaponsCanvas instance;
    public static WeaponsCanvas Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    public void RetrunFromSpecificWeaponMenu()
    {
        SpecificWeaponMenu.SetActive(false);
        WeaponsMenu.SetActive(true);
    }
    public void RetrunFromSpecificModMenu()
    {
        specificModMenu.SetActive(false);
        OpenSpecificWeaponMenu(clickedWeapon);
    }

    public void OpenSpecificWeaponMenu(Weapon _clickedWeapon)
    {
        clickedWeapon = _clickedWeapon;
        WeaponsMenu.SetActive(false);
        SpecificWeaponMenu.SetActive(true);
        specificWeaponTitle.text = _clickedWeapon.weaponName;
    }
    public void OpenSpecificModMenu(int modType)
    {
        SpecificWeaponMenu.SetActive(false);
        specificModMenu.SetActive(true);

        if (modType == (int)WeaponModType.muzzle)
        {
            InstanciateButtonsForMods(clickedWeapon.muzzleMods);
        }
        else if (modType == (int)WeaponModType.scope)
        {
            InstanciateButtonsForMods(clickedWeapon.scopeMods);
        }
        else if (modType == (int)WeaponModType.magazine)
        {
            InstanciateButtonsForMods(clickedWeapon.magazineMods);
        }
        else if (modType == (int)WeaponModType.special)
        {
            InstanciateButtonsForMods(clickedWeapon.specialMods);
        }
        else
        {
            Debug.LogWarning("Mod Button sent a wrong number");
        }

        specifiModTitle.text = clickedWeapon.weaponName + " " + (WeaponModType)modType + "s";
    }

    void InstanciateButtonsForMods(WeaponMod[ ] weaponMods)
    {
        foreach (WeaponMod weaponMod in weaponMods)
        {
            Instantiate(ModButtonPrefab, modVerticalLayout.transform);
        }
    }
}

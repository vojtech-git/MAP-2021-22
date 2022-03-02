using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
    [Header("To Disable")]
    public Mouse cameraController;
    public GunScript[] guns;
    public UIUpgradeEnablerer uiUpgradeEnablerer;

    [HideInInspector] public Weapon selectedWeapon;
    [HideInInspector] public bool menuOpen = false;

    private static WeaponsCanvas instance;
    public static WeaponsCanvas Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!menuOpen)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }

    public void OpenMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        WeaponsMenu.SetActive(true);
        SpecificWeaponMenu.SetActive(false);
        specificModMenu.SetActive(false);

        menuOpen = true;

        // disabling mouse control and guns
        foreach (GunScript gun in guns)
        {
            gun.enabled = false;
        }
        cameraController.enabled = false;
        uiUpgradeEnablerer.EnableUI();
        Time.timeScale = 0f;
    }
    public void CloseMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        WeaponsMenu.SetActive(false);
        SpecificWeaponMenu.SetActive(false);
        specificModMenu.SetActive(false);

        menuOpen = false;

        // enabling mouse control and guns
        foreach (GunScript gun in guns)
        {
            gun.enabled = true;
        }
        cameraController.enabled = true;
        uiUpgradeEnablerer.DisableUI();
        Time.timeScale = 1f;
    }

    public void OpenSpecificWeaponMenu(Weapon _selectedWeapon)
    {
        selectedWeapon = _selectedWeapon;
        WeaponsMenu.SetActive(false);
        SpecificWeaponMenu.SetActive(true);
        specificWeaponTitle.text = _selectedWeapon.weaponName;
    }
    public void RetrunFromSpecificWeaponMenu()
    {
        SpecificWeaponMenu.SetActive(false);
        WeaponsMenu.SetActive(true);
    }

    public void OpenSpecificModMenu(int modType)
    {
        foreach (Transform transform in modVerticalLayout.transform)
        {
            Destroy(transform.gameObject);
        }

        SpecificWeaponMenu.SetActive(false);
        specificModMenu.SetActive(true);

        if (modType == (int)WeaponModType.muzzle)
        {
            InstanciateButtonsForMods(selectedWeapon.muzzleMods);
        }
        else if (modType == (int)WeaponModType.scope)
        {
            InstanciateButtonsForMods(selectedWeapon.scopeMods);
        }
        else if (modType == (int)WeaponModType.magazine)
        {
            InstanciateButtonsForMods(selectedWeapon.magazineMods);
        }
        else if (modType == (int)WeaponModType.special)
        {
            InstanciateButtonsForMods(selectedWeapon.specialMods);
        }
        else
        {
            Debug.LogWarning("Mod Button sent a wrong number");
        }

        specifiModTitle.text = selectedWeapon.weaponName + " " + (WeaponModType)modType + "s";
    }
    public void RetrunFromSpecificModMenu()
    {
        specificModMenu.SetActive(false);
        OpenSpecificWeaponMenu(selectedWeapon);
    }

    void InstanciateButtonsForMods(WeaponMod[] weaponMods)
    {
        foreach (WeaponMod weaponMod in weaponMods)
        {
            GameObject buttonGameObject = Instantiate(ModButtonPrefab, modVerticalLayout.transform);
            buttonGameObject.GetComponent<WeaponModButton>().mod = weaponMod;
            buttonGameObject.GetComponentInChildren<Text>().text = weaponMod.name;
        }
    }
}

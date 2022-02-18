using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public Weapon weaponScriptableObj;

    [Header("Basic Weapon Stats")]
    public int basicDamage;
    public float basicTimeBetweenShooting;
    public float basicSpread;
    public float basicRange;
    public float basicReloadTime;
    public float basicTimeBetweenShots;
    public int basicMagazineSize;
    public int basicBulletsPerTap;
    public bool basicAllowButtonHold;

    [Header("Upgraded Weapon Stats")]
    [HideInInspector] public int damage;
    [HideInInspector] public float timeBetweenShooting;
    [HideInInspector] public float spread;
    [HideInInspector] public float range;
    [HideInInspector] public float reloadTime;
    [HideInInspector] public float timeBetweenShots;
    [HideInInspector] public int magazineSize;
    [HideInInspector] public int bulletsPerTap;
    [HideInInspector] public int bulletsShot;
    [HideInInspector] public int bulletsMags;
    [HideInInspector] public bool allowButtonHold;

    bool shooting, readyToShoot, reloading;
    [HideInInspector] public int bulletsLeft;

    [Header("Mod Parents")]
    public Transform WeaponWheelParent;
    public Transform[] modModelParents; // kazdej typ modu musi mit svuj parent 

    [Header("Assignebles")]
    public Camera PlayerCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public Text AmmoCount;
    public Text AmmoBack;
    public GameObject bulletHoleGraphic;
    public ParticleSystem muzzleFlash, bulletDrop;

    [Header("Upgrades")]
    public GameObject[] mags;
    public GameObject[] scopes;
    public GameObject[] muzzles;
    public GameObject[] specials;

    public void ApplyUpgradedStats()
    {
        damage = basicDamage;
        timeBetweenShooting = basicTimeBetweenShooting;
        spread = basicSpread;
        range = basicRange;
        reloadTime = basicReloadTime;
        timeBetweenShots = basicTimeBetweenShots;
        magazineSize = basicMagazineSize;
        bulletsPerTap = basicBulletsPerTap;
        allowButtonHold = basicAllowButtonHold;

        for (int i = 0; i < weaponScriptableObj.equippedMods.Length; i++)
        {
            if (weaponScriptableObj.equippedMods[i] != null)
            {
                damage += weaponScriptableObj.equippedMods[i].damage;
                timeBetweenShooting += weaponScriptableObj.equippedMods[i].timeBetweenShots;
                spread += weaponScriptableObj.equippedMods[i].spread;
                range += weaponScriptableObj.equippedMods[i].range;
                reloadTime += weaponScriptableObj.equippedMods[i].reloadTime;
                timeBetweenShots += weaponScriptableObj.equippedMods[i].timeBetweenShots;
                magazineSize += weaponScriptableObj.equippedMods[i].magazineSize;
                bulletsPerTap += weaponScriptableObj.equippedMods[i].bulletsPerTap;
                allowButtonHold = weaponScriptableObj.equippedMods[i].allowButtonHold;
            }
        }

        #region old logic
        //// vžydcky jde do basic aby se to nestackovalo
        // presunul jsem

        //// pridava k upgraded
        //foreach (var item in mags)
        //{
        //    if (item.activeInHierarchy == true)
        //    {
        //        int upgradedDamage = item.GetComponent<GunUpgrade>().upgradedDamage;
        //        this.upgradedDamage += upgradedDamage;

        //        float upgradedTimeBetweenShooting = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShooting;
        //        timeBetweenShooting += upgradedTimeBetweenShooting;

        //        float upgradedSpread = item.GetComponent<GunUpgrade>().upgradedSpread;
        //        spread += upgradedSpread;

        //        float upgradedRange = item.GetComponent<GunUpgrade>().upgradedRange;
        //        range += upgradedRange;

        //        float upgradedReloadTime = item.GetComponent<GunUpgrade>().upgradedReloadTime;
        //        reloadTime += upgradedReloadTime;

        //        float upgradedTimeBetweenShots = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShots;
        //        timeBetweenShots += upgradedTimeBetweenShots;

        //        int upgradedMagazineSize = item.GetComponent<GunUpgrade>().upgradedMagazineSize;
        //        magazineSize += upgradedMagazineSize;

        //        int upgradedBulletsPerTap = item.GetComponent<GunUpgrade>().upgradedBulletsPerTap;
        //        bulletsPerTap += upgradedBulletsPerTap;
        //    }
        //}
        //foreach (var item in scopes)
        //{

        //    if (item.activeInHierarchy == true)
        //    {
        //        int upgradedDamage = item.GetComponent<GunUpgrade>().upgradedDamage;
        //        this.upgradedDamage += upgradedDamage;

        //        float upgradedTimeBetweenShooting = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShooting;
        //        timeBetweenShooting += upgradedTimeBetweenShooting;

        //        float upgradedSpread = item.GetComponent<GunUpgrade>().upgradedSpread;
        //        spread += upgradedSpread;

        //        float upgradedRange = item.GetComponent<GunUpgrade>().upgradedRange;
        //        range += upgradedRange;

        //        float upgradedReloadTime = item.GetComponent<GunUpgrade>().upgradedReloadTime;
        //        reloadTime += upgradedReloadTime;

        //        float upgradedTimeBetweenShots = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShots;
        //        timeBetweenShots += upgradedTimeBetweenShots;

        //        int upgradedMagazineSize = item.GetComponent<GunUpgrade>().upgradedMagazineSize;
        //        magazineSize += upgradedMagazineSize;

        //        int upgradedBulletsPerTap = item.GetComponent<GunUpgrade>().upgradedBulletsPerTap;
        //        bulletsPerTap = upgradedBulletsPerTap;
        //    }
        //}
        //foreach (var item in muzzles)
        //{
        //    if (item.activeInHierarchy == true)
        //    {
        //        int upgradedDamage = item.GetComponent<GunUpgrade>().upgradedDamage;
        //        this.upgradedDamage += upgradedDamage;

        //        float upgradedTimeBetweenShooting = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShooting;
        //        timeBetweenShooting += upgradedTimeBetweenShooting;

        //        float upgradedSpread = item.GetComponent<GunUpgrade>().upgradedSpread;
        //        spread += upgradedSpread;

        //        float upgradedRange = item.GetComponent<GunUpgrade>().upgradedRange;
        //        range += upgradedRange;

        //        float upgradedReloadTime = item.GetComponent<GunUpgrade>().upgradedReloadTime;
        //        reloadTime += upgradedReloadTime;

        //        float upgradedTimeBetweenShots = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShots;
        //        timeBetweenShots += upgradedTimeBetweenShots;

        //        int upgradedMagazineSize = item.GetComponent<GunUpgrade>().upgradedMagazineSize;
        //        magazineSize += upgradedMagazineSize;

        //        int upgradedBulletsPerTap = item.GetComponent<GunUpgrade>().upgradedBulletsPerTap;
        //        bulletsPerTap = upgradedBulletsPerTap;
        //    }
        //}
        //foreach (var item in specials)
        //{
        //    if (item.activeInHierarchy == true)
        //    {
        //        int upgradedDamage = item.GetComponent<GunUpgrade>().upgradedDamage;
        //        this.upgradedDamage += upgradedDamage;

        //        float upgradedTimeBetweenShooting = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShooting;
        //        timeBetweenShooting += upgradedTimeBetweenShooting;

        //        float upgradedSpread = item.GetComponent<GunUpgrade>().upgradedSpread;
        //        spread += upgradedSpread;

        //        float upgradedRange = item.GetComponent<GunUpgrade>().upgradedRange;
        //        range += upgradedRange;

        //        float upgradedReloadTime = item.GetComponent<GunUpgrade>().upgradedReloadTime;
        //        reloadTime += upgradedReloadTime;

        //        float upgradedTimeBetweenShots = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShots;
        //        timeBetweenShots += upgradedTimeBetweenShots;

        //        int upgradedMagazineSize = item.GetComponent<GunUpgrade>().upgradedMagazineSize;
        //        magazineSize += upgradedMagazineSize;

        //        int upgradedBulletsPerTap = item.GetComponent<GunUpgrade>().upgradedBulletsPerTap;
        //        bulletsPerTap = upgradedBulletsPerTap;
        //    }
        //}
        #endregion
    }

    public void ApplyModGraphics()
    {
        // clearnout ted aktivní grafiku

        for (int i = 0; i < weaponScriptableObj.equippedMods.Length; i++)
        {
            if (weaponScriptableObj.equippedMods[i].model != null)
            {
                Instantiate(weaponScriptableObj.equippedMods[i].model, modModelParents[i]);

                Debug.Log("Applying " + weaponScriptableObj.equippedMods[i].name + " to parent: " + modModelParents[i].gameObject.name + " on gun: " + gameObject.name); 
            }
            else
            {
                Debug.Log("No " + (WeaponModType)i + " to create graphics for");
            }
        }
    }

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

        WeaponManager.onModEquipped += OnModEquipped;
    }

    private void Start()
    {
        ApplyUpgradedStats();
        ApplyModGraphics();

        for (int i = 0; i < weaponScriptableObj.equippedMods.Length; i++)
        {
            if (weaponScriptableObj.equippedMods[i] != null)
            {
                Debug.Log(gameObject.name + " " + (WeaponModType)i + " equppied: " + weaponScriptableObj.equippedMods[i].name);
            }
            else
            {
                Debug.Log("No " + (WeaponModType)i + " equipped on " + gameObject.name);
            }
        }
    }

    private void Update()
    {
        if(PauseMenu.GameIsPaused == false)
        {
            MyInput();
            AmmoCount.text = "" + bulletsLeft;
            AmmoBack.text = "" + bulletsMags;
        }
    }

    private void OnDestroy()
    {
        WeaponManager.onModEquipped -= OnModEquipped;
    }

    public void OnModEquipped(Weapon weapon, WeaponMod weaponMod)
    {
        if (weapon == this.weaponScriptableObj)
        {
            ApplyUpgradedStats();
            ApplyModGraphics();
        }
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading && bulletsMags - (magazineSize - bulletsLeft) >= 0) Reload();

        //Shoot
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calc Direction with Spread
        Vector3 direction = PlayerCam.transform.forward + new Vector3(x, y, 0);

        //Running Spead to do

        //RayCast
        if (Physics.Raycast(PlayerCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            //Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                Entity hitTarget;
                rayHit.collider.gameObject.TryGetComponent<Entity>(out hitTarget);
                hitTarget.TakeDamage(damage);

                Debug.Log("damaging target for " + damage);

             // tady se volá enemy hit   rayHit.collider.GetComponent<ShootingAi>().TakeDamage(Damage);
            }
        }
        //Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        muzzleFlash.Play();
        bulletDrop.Play();


        // Yeeters (bulleftShot nefunguje?? whaat?)
        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);


        if (bulletsShot > 0 && bulletsLeft > 0)
        Invoke("Shoot", timeBetweenShots);

    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsMags = bulletsMags - (magazineSize - bulletsLeft);
        bulletsLeft = magazineSize;
        reloading = false;
    }

}

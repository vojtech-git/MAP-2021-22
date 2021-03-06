using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public Weapon weaponScriptableObj;
    public bool isSpecial;

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
    public int basicBulletsMags;

    [Header("Upgraded Weapon Stats")]
    public int damage;
    public float timeBetweenShooting;
    public float spread;
    public float range;
    public float reloadTime;
    public float timeBetweenShots;
    public int magazineSize;
    public int bulletsPerTap;
    public int bulletsShot;
    public int bulletsMags;
    public bool allowButtonHold;

    bool shooting, readyToShoot, reloading;
     public int bulletsLeft;

    [Header("Mod Parents")]
    public Transform WeaponWheelParent;
    public Transform[] modModelParents; // kazdej typ modu musi mit svuj parent 

    [Header("Assignebles")]
    public Camera PlayerCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask isHittable;
    public Text AmmoCount;
    public Text AmmoBack;
    public GameObject bulletHoleGraphic;
    public ParticleSystem muzzleFlash, bulletDrop;

    [Header("Upgrades")]
    public GameObject[] mags;
    public GameObject[] scopes;
    public GameObject[] muzzles;
    public GameObject[] specials;
    public AudioSource pistolSound;

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
        bulletsMags = basicBulletsMags;

        if (!isSpecial)
        {
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
                    bulletsMags += weaponScriptableObj.equippedMods[i].bulletsMags;
                }
            } 
        }

        #region old logic
        //// v???ydcky jde do basic aby se to nestackovalo
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
        if (!isSpecial)
        {
            foreach (Transform weaponWheelChild in WeaponWheelParent)
            {
                Destroy(weaponWheelChild.gameObject);
            }

            for (int i = 0; i < weaponScriptableObj.equippedMods.Length; i++)
            {
                if (weaponScriptableObj.equippedMods[i] != null)
                {
                    if (weaponScriptableObj.equippedMods[i].ui != null)
                    {
                        Instantiate(weaponScriptableObj.equippedMods[i].ui, WeaponWheelParent);

                        //Debug.Log("Applying weapon wheel graphics for mod: " + weaponScriptableObj.equippedMods[i].name + " to parent: " + modModelParents[i].gameObject.name + " on gun: " + gameObject.name);
                    }
                    else
                    {
                        //Debug.Log("No ui graphics to create graphics for " + gameObject.name + " " + (WeaponModType)i);
                    }
                }
                else
                {
                    //Debug.Log("No mod eqquipped in this slot (gun ui)");
                }

            }


            // clearnout ted aktivn??? grafiku
            foreach (Transform transform in modModelParents)
            {
                foreach (Transform transform1 in transform)
                {
                    Destroy(transform1.gameObject);
                }
            }

            for (int i = 0; i < weaponScriptableObj.equippedMods.Length; i++)
            {
                if (weaponScriptableObj.equippedMods[i] != null)
                {
                    if (weaponScriptableObj.equippedMods[i].model != null)
                    {
                        Instantiate(weaponScriptableObj.equippedMods[i].model, modModelParents[i]);

                        //Debug.Log("Applying " + weaponScriptableObj.equippedMods[i].name + " to parent: " + modModelParents[i].gameObject.name + " on gun: " + gameObject.name);
                    }
                    else
                    {
                        //Debug.Log("No " + (WeaponModType)i + " to create graphics for");
                    }
                }
                else
                {
                    //Debug.Log("no mod equipped in this slot (gun model)");
                }
            } 
        }
    }

    private void Awake()
    {
        WeaponManager.onModEquipped += OnModEquipped;

        ApplyUpgradedStats();
        ApplyModGraphics();

        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Start()
    {

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

        RaycastHit hit;

        //RayCast
        if (Physics.Raycast(PlayerCam.transform.position, direction, out hit, range, isHittable))
        {
            //Debug.Log(hit.collider.name);

            if (!hit.collider.CompareTag("Friendly") && hit.transform.TryGetComponent<Entity>(out Entity entityToDamage))
            {
                entityToDamage.TakeDamage(damage);
            }

            if (!hit.transform.TryGetComponent<Entity>(out Entity entityToNotBullethole))
            {
                GameObject bullethole = Instantiate(bulletHoleGraphic, hit.point, Quaternion.LookRotation(hit.normal));
                bullethole.transform.parent = hit.transform;
                bullethole.transform.position += bullethole.transform.forward / 1000;
            }
        }
        //Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        muzzleFlash.Play();
        bulletDrop.Play();
        pistolSound.Play();

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

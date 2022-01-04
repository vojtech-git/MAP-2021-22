using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ParticleSystemJobs;

public class GunScript : MonoBehaviour
{
    [Header("Basic Weapon Stats")]
    public int basicDamage;
    public float basicTimeBetweenShooting;
    public float basicSpread;
    public float basicRange;
    public float basicReloadTime;
    public float basicTimeBetweenShots;
    public int basicMagazineSize;
    public int basicBulletsPerTap;

    [Header ("Upgraded Weapon Stats")]
    public int damage;
    public float timeBetweenShooting;
    public float spread;
    public float range;
    public float reloadTime;
    public float timeBetweenShots;
    public int magazineSize;
    public int bulletsPerTap;
    public bool allowButtonHold;
    public int bulletsLeft, bulletsShot;
    public int bulletsMags;

    bool shooting, readyToShoot, reloading;

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

    public void Upgrades()
    {
        // vžydcky jde do basic aby se to nestackovalo
        damage = basicDamage;
        timeBetweenShooting = basicTimeBetweenShooting;
        spread = basicSpread;
        range = basicRange;
        reloadTime = basicReloadTime;
        timeBetweenShots = basicTimeBetweenShots;
        magazineSize = basicMagazineSize;
        timeBetweenShots = basicTimeBetweenShots;
        magazineSize = basicMagazineSize;
        bulletsPerTap = basicBulletsPerTap;

        // pridava k upgraded
        foreach (var item in mags)
        {
            if (item.activeInHierarchy == true)
            {
                int upgradedDamage = item.GetComponent<GunUpgrade>().upgradedDamage;
                damage += upgradedDamage;

                float upgradedTimeBetweenShooting = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShooting;
                timeBetweenShooting += upgradedTimeBetweenShooting;

                float upgradedSpread = item.GetComponent<GunUpgrade>().upgradedSpread;
                spread += upgradedSpread;

                float upgradedRange = item.GetComponent<GunUpgrade>().upgradedRange;
                range += upgradedRange;

                float upgradedReloadTime = item.GetComponent<GunUpgrade>().upgradedReloadTime;
                reloadTime += upgradedReloadTime;

                float upgradedTimeBetweenShots = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShots;
                timeBetweenShots += upgradedTimeBetweenShots;

                int upgradedMagazineSize = item.GetComponent<GunUpgrade>().upgradedMagazineSize;
                magazineSize += upgradedMagazineSize;

                int upgradedBulletsPerTap = item.GetComponent<GunUpgrade>().upgradedBulletsPerTap;
                bulletsPerTap += upgradedBulletsPerTap;
            }
        }
        foreach (var item in scopes)
        {
            if (item.activeInHierarchy == true)
            {
                int upgradedDamage = item.GetComponent<GunUpgrade>().upgradedDamage;
                damage += upgradedDamage;

                float upgradedTimeBetweenShooting = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShooting;
                timeBetweenShooting += upgradedTimeBetweenShooting;

                float upgradedSpread = item.GetComponent<GunUpgrade>().upgradedSpread;
                spread += upgradedSpread;

                float upgradedRange = item.GetComponent<GunUpgrade>().upgradedRange;
                range += upgradedRange;

                float upgradedReloadTime = item.GetComponent<GunUpgrade>().upgradedReloadTime;
                reloadTime += upgradedReloadTime;

                float upgradedTimeBetweenShots = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShots;
                timeBetweenShots += upgradedTimeBetweenShots;

                int upgradedMagazineSize = item.GetComponent<GunUpgrade>().upgradedMagazineSize;
                magazineSize += upgradedMagazineSize;

                int upgradedBulletsPerTap = item.GetComponent<GunUpgrade>().upgradedBulletsPerTap;
                bulletsPerTap = upgradedBulletsPerTap;
            }
        }
        foreach (var item in muzzles)
        {
            if (item.activeInHierarchy == true)
            {
                int upgradedDamage = item.GetComponent<GunUpgrade>().upgradedDamage;
                damage += upgradedDamage;

                float upgradedTimeBetweenShooting = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShooting;
                timeBetweenShooting += upgradedTimeBetweenShooting;

                float upgradedSpread = item.GetComponent<GunUpgrade>().upgradedSpread;
                spread += upgradedSpread;

                float upgradedRange = item.GetComponent<GunUpgrade>().upgradedRange;
                range += upgradedRange;

                float upgradedReloadTime = item.GetComponent<GunUpgrade>().upgradedReloadTime;
                reloadTime += upgradedReloadTime;

                float upgradedTimeBetweenShots = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShots;
                timeBetweenShots += upgradedTimeBetweenShots;

                int upgradedMagazineSize = item.GetComponent<GunUpgrade>().upgradedMagazineSize;
                magazineSize += upgradedMagazineSize;

                int upgradedBulletsPerTap = item.GetComponent<GunUpgrade>().upgradedBulletsPerTap;
                bulletsPerTap = upgradedBulletsPerTap;
            }
        }
        foreach (var item in specials)
        {
            if (item.activeInHierarchy == true)
            {
                int upgradedDamage = item.GetComponent<GunUpgrade>().upgradedDamage;
                damage += upgradedDamage;

                float upgradedTimeBetweenShooting = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShooting;
                timeBetweenShooting += upgradedTimeBetweenShooting;

                float upgradedSpread = item.GetComponent<GunUpgrade>().upgradedSpread;
                spread += upgradedSpread;

                float upgradedRange = item.GetComponent<GunUpgrade>().upgradedRange;
                range += upgradedRange;

                float upgradedReloadTime = item.GetComponent<GunUpgrade>().upgradedReloadTime;
                reloadTime += upgradedReloadTime;

                float upgradedTimeBetweenShots = item.GetComponent<GunUpgrade>().upgradedTimeBetweenShots;
                timeBetweenShots += upgradedTimeBetweenShots;

                int upgradedMagazineSize = item.GetComponent<GunUpgrade>().upgradedMagazineSize;
                magazineSize += upgradedMagazineSize;

                int upgradedBulletsPerTap = item.GetComponent<GunUpgrade>().upgradedBulletsPerTap;
                bulletsPerTap = upgradedBulletsPerTap;
            }
        }
    }
    private void Awake()
    {
        Upgrades();
        bulletsLeft = magazineSize;
        readyToShoot = true;
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
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                Entity hitTarget;
                rayHit.collider.gameObject.TryGetComponent<Entity>(out hitTarget);
                hitTarget.TakeDamage(damage);

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

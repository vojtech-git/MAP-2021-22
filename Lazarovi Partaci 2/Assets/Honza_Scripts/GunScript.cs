using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ParticleSystemJobs;

public class GunScript : MonoBehaviour
{
    [Header ("Weapon Stats")]
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
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

    private void Awake()
    {
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

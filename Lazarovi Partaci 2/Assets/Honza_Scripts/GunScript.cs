using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
        AmmoCount.text = ""+ bulletsLeft;
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading && bulletsMags - magazineSize >= 0) Reload();

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

        //Running Spead to do

        //RayCast
        if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
             // tady se volá enemy hit   rayHit.collider.GetComponent<ShootingAi>().TakeDamage(Damage);
            }
        }

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
        bulletsMags = bulletsMags - magazineSize;
        bulletsLeft = magazineSize;
        reloading = false;
    }

}

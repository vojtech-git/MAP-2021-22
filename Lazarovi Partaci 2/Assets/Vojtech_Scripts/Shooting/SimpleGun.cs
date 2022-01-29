using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGun : MonoBehaviour
{
    [Header("Stats")]
    public float damage;
    public int magazineSize;
    public float reloadTime;
    public float timeBetweenShots;
    public LayerMask isHittable;

    [Header("Setup")]
    public GameObject mainCamera;
    public GameObject bulletHoleGraphic;

    private bool readyToShoot = true;
    private int bulletsInMagazine;
    private bool reloading = false;

    private void Start()
    {
        bulletsInMagazine = magazineSize;        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (readyToShoot && !reloading)
        {
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 10000, isHittable))
            {
                Debug.Log(hit.collider.name);

                if (hit.collider.CompareTag("Enemy"))
                {
                    Entity hitTarget;
                    hit.collider.gameObject.TryGetComponent<Entity>(out hitTarget);
                    hitTarget.TakeDamage(damage);
                }
            }

            GameObject bullethole =  Instantiate(bulletHoleGraphic, hit.point, Quaternion.LookRotation(hit.normal));
            bullethole.transform.parent = hit.transform;
            bullethole.transform.position += bullethole.transform.forward / 1000;

            bulletsInMagazine--;

            if (bulletsInMagazine <= 0)
                StartCoroutine(Reload());
            else
                StartCoroutine(DelayAttack());
        }
    }

    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);

        bulletsInMagazine = magazineSize;

        reloading = false;
    }

    IEnumerator DelayAttack()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        readyToShoot = true;
    }
}

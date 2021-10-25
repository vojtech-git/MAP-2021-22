using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeShooting : MonoBehaviour
{
   public GameObject bullet;
   public float shootForce;
   public float spread, reloadTime, timeBetweenShots;
   public int magazineSize, bulletsPerTap;
   public bool allowButtonHold;
   int bulletsLeft, bulletsShot;
   bool shooting, readyToShoot, reloading;
   public Camera kamera;
   public Transform shootPoint;
    public Transform shootPoint2;

   public bool allowInvoke = true;
   public GameObject muzzleFlash;

   public AudioSource laserSound;


    private void Awake(){
        bulletsLeft=magazineSize;
        readyToShoot=true;
    }
    void Update()
    {
       /*   if (Input.GetButton("Fire1"))
        {  
             if(!laserSound.isPlaying)
                 laserSound.Play();      
        } */
        myInput();
       
    }

    private void myInput(){
        if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(Input.GetKey(KeyCode.R) && bulletsLeft < magazineSize && !reloading)Reload();

        if(readyToShoot && shooting && !reloading && bulletsLeft<=0) Reload();

        if(readyToShoot && shooting && !reloading && bulletsLeft>0) {
            bulletsShot = 0;
            Shoot();
           
        }   
    }

    private void Shoot(){
        readyToShoot = false;
       
       
        //  Ray ray = kamera.ViewportPointToRay(new Vector3(0.5f, 0.5f,0));    //PRO STRELBU DOPROSTRED OBRAZOVKY TOHLE ODKOMENTOVAT
        //  RaycastHit hit;  
        //  Vector3 targetPoint;
        //  if(Physics.Raycast(ray, out hit)){
        //      targetPoint = hit.point;
        //  }
        //  else {
        //      targetPoint = ray.GetPoint(75);
        //  }

        //  Vector3 directionWithoutSpread = targetPoint - shootPoint.position;
         
        //  Vector3 directionWithoutSpread2 = targetPoint - shootPoint2.position;

        Vector3 directionWithoutSpread =  shootPoint.forward;
        Vector3 directionWithoutSpread2 = shootPoint2.forward;

         GameObject currentBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
         GameObject currentBullet2 = Instantiate(bullet, shootPoint2.position, Quaternion.identity);

          currentBullet.transform.forward = directionWithoutSpread.normalized;
          currentBullet2.transform.forward = directionWithoutSpread2.normalized;
         currentBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
          currentBullet2.GetComponent<Rigidbody>().AddForce(shootPoint2.forward * shootForce, ForceMode.Impulse);
        if(muzzleFlash !=null){
            Instantiate(muzzleFlash, shootPoint.position, Quaternion.identity);  //STRELBA Z DVOU KANONU ROVNE. PRO STRELUBU 
        }


          bulletsLeft--;
         bulletsShot++;


        if(allowInvoke){
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke=false;

        }





//--------------------------------------------------------------


        // GameObject bullet = Instantiate(laserPrefab);
        // GameObject bullet2 = Instantiate(laserPrefab);

        // Physics.IgnoreCollision(bullet.GetComponent<Collider>(), shuttleCollider.GetComponent<Collider>());
        // Physics.IgnoreCollision(bullet2.GetComponent<Collider>(), shuttleCollider.GetComponent<Collider>());

        // bullet.transform.position = laserEmitter.position;
        // bullet2.transform.position = laserEmitter2.position;
        // /*Vector3 rotation = bullet.transform.rotation.eulerAngles;
        // Vector3 rotation2 = bullet2.transform.rotation.eulerAngles;
        // bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        // bullet2.transform.rotation = Quaternion.Euler(rotation2.x, transform.eulerAngles.y, rotation2.z);*/

        // bullet.GetComponent<Rigidbody>().AddForce(laserEmitter.forward * laserSpeed, ForceMode.Impulse);
        // bullet2.GetComponent<Rigidbody>().AddForce(laserEmitter.forward * laserSpeed, ForceMode.Impulse);
    }

    private void ResetShot(){
        readyToShoot=true;
        allowInvoke=true;
    }

    private void Reload(){
        reloading = true;
        Invoke("ReloadFinished", reloadTime);

    }
    private void ReloadFinished(){
        bulletsLeft = magazineSize;
        reloading = false;

    }
}

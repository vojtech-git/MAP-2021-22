using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseShooting : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bullet;
    public float game_z = 8;
    public float shootForce;
    public Transform shootPoint;
    public Transform shootPoint2;
     Vector3 targetPoint;

     public bool allowButtonHold;

    public bool allowInvoke = true;
    public float spread, reloadTime, timeBetweenShots;
     bool  readyToShoot, reloading;
     bool shooting;
    
    private void Awake(){
         readyToShoot=true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
              
    //   if (Input.GetButtonDown("Fire1")){
    //       Shoot();
    //   }

      myInput();

    }

    private void myInput(){
 if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);


        if(readyToShoot && shooting && !reloading){
            Shoot();
        }
    }

     private void Shoot(){
          readyToShoot = false;  //IDK WTF, NEKDY TO FUNGUJE, CO TO DO PÍČI JE ALE KURVA PROČ TEN SHOOTFORCE MUSI BYT TAK MALEJ KDYZ DOSLOVA V JINYM SKRIPTU MAM 300 A FUNGUJE TO DOBRE
            Physics.IgnoreLayerCollision(8,8, true);
            RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit)){
              
              targetPoint = hit.point;
              Debug.Log( "tvuj shootifrce" +shootForce);
        
             }
             else{
                  targetPoint = ray.GetPoint(250); //URCUJE VZDALENOST KDE SE NABOJE ROZUTIKAJI DO STRAN
                 
             }

        //  Vector2 screen_mouse_pos = Input.mousePosition;
        //  Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(screen_mouse_pos.x, screen_mouse_pos.y, game_z));
         
     
            Vector3 bullet_direction = targetPoint - shootPoint.position;
            Vector3 bullet_direction2 = targetPoint - shootPoint2.position;
            GameObject fired_bullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            GameObject fired_bullet2 = Instantiate(bullet, shootPoint2.position, Quaternion.identity);
            fired_bullet.transform.forward = bullet_direction.normalized;
            fired_bullet2.transform.forward = bullet_direction2.normalized;
            // fired_bullet.GetComponent<Rigidbody>().velocity += velocity * bullet_direction;
            fired_bullet.GetComponent<Rigidbody>().AddForce(bullet_direction * shootForce, ForceMode.Impulse);
            fired_bullet2.GetComponent<Rigidbody>().AddForce(bullet_direction2 * shootForce, ForceMode.Impulse);
         


              if(allowInvoke){
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke=false;

        }
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
       
        reloading = false;

    }

    }


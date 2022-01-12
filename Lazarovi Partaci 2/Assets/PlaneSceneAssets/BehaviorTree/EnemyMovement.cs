using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float forwardThrustPower = 1000f;
    public float yawSpeed;
     IController thisInput;
    Rigidbody myRigidbody;
    public GameObject pilot;

    public float maxVelocity = 500f;

    //-----------------------------------SHOOTING
    
    public Transform shootPoint;
    public Transform shootPoint2;
    public GameObject bullet;
    public float shootForce;
    public float timeBetweenShots;
    public bool readyToShoot;
    public bool allowInvoke = true;

    // -------------------------------------KONEC SHOOTING PROMENNYCH
  


 /*   public  event InputEvent ForwardEvent;
   public  event InputEvent TurnEvent; */
    void Awake()
    {   myRigidbody = GetComponent<Rigidbody>();
        readyToShoot = true;
      /*  thisInput.ForwardEvent += ForwardThrust;
       thisInput.TurnEvent += TurnToTarget;
 */

       if(pilot)
       {
           thisInput = pilot.GetComponent<IController>();
           thisInput.ForwardEvent += ForwardThrust;
           thisInput.TurnEvent += TurnToTarget;
           thisInput.FireEvent += FireWeapon;
       }
       else {
           Debug.LogError("NO pilot on", gameObject);
       }
    } 


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ForwardThrust(float thrust)
    {
        myRigidbody.AddForce(gameObject.transform.forward * thrust * forwardThrustPower * Time.deltaTime);


        if(myRigidbody.velocity.magnitude > maxVelocity)
        {
            myRigidbody.velocity = myRigidbody.velocity.normalized * maxVelocity;
        }
    }

    private void TurnToTarget(float x, float y, float z)
    {
        Vector3 desiredHeading = new Vector3 (x,y,z);
        Quaternion rotationGoal = Quaternion.LookRotation(desiredHeading);
        float step = yawSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationGoal, step);

    }


    private void FireWeapon()
    {
        if (readyToShoot == true){
        readyToShoot = false; 
        Vector3 directionWithoutSpread = shootPoint.forward;
        Vector3 directionWithoutSpread2 = shootPoint2.forward;

        GameObject currentBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        GameObject currentBullet2 = Instantiate(bullet, shootPoint2.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithoutSpread.normalized;
        currentBullet2.transform.forward = directionWithoutSpread2.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet2.GetComponent<Rigidbody>().AddForce(directionWithoutSpread2.normalized * shootForce, ForceMode.Impulse);
       


        StartCoroutine(ShootDelay());
        }
       /*  if (allowInvoke) {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;

        } */
    }
     IEnumerator ShootDelay()
   {
     yield return new WaitForSeconds(timeBetweenShots);
     readyToShoot = true;
   }
      public void ResetShot(){
        readyToShoot=true;
        allowInvoke=true;
    }

}

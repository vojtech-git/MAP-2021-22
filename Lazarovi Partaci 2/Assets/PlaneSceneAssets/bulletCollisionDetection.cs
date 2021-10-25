using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollisionDetection : MonoBehaviour
{   
 


   
    // Start is called before the first frame update
  void OnCollisionEnter(Collision col){
    //Physics.IgnoreLayerCollision() PRIDAT PRO IGNOROVANI PLAYERA A NABOJE
    Physics.IgnoreLayerCollision(8,8, true);
    
    if(col.gameObject!=null){
      if(col.gameObject.tag=="Enemy"){
        Debug.Log("TREFIL JSEM TĚ");
        col.gameObject.GetComponent<HealthEnemy>().enemyHP-=40;
        //Debug.Log(col.gameObject.GetComponent<HealthEnemy>().enemyHP);
          //Debug.Log(a.enemyHP);
         // Debug.Log(enemyHealth.enemyHP);
          //enemyHealth.enemyHP-=40;
          Destroy(this.gameObject);  //FUNGUJE ODEBIRANI HP NEPRATELUM
      }
       if (col.gameObject.layer == 8)
      {
          Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>());
          Debug.Log("NABOJ NABOJ MYJE");
       }

    else {
      Destroy(this.gameObject);
    }
     
  }

 
}
}

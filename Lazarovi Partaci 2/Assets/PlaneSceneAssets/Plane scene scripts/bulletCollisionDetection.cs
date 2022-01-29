using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollisionDetection : MonoBehaviour
{
        private void Start()
        {
            Physics.IgnoreLayerCollision(0, 9);
            Physics.IgnoreLayerCollision(2, 9);
             
        }

      // COLLIDER HRACE MUZEM NECHAT JAK JE, DALI JSME SHOOTPOINTY ENEMAKA K SOBE TAKZE ACTUALLY TARGETUJE COLLIDER HRACE
      // MUSIME VYRESIT, ABY RAYCAST IGNOROVAL PLAYER LAYER.

    // Start is called before the first frame update
    void OnTriggerEnter(Collider col) {
        //Physics.IgnoreLayerCollision() PRIDAT PRO IGNOROVANI PLAYERA A NABOJE
      /*   Physics.IgnoreLayerCollision(9, 9, true);
        Physics.IgnoreLayerCollision(0, 9, true);
        Physics.IgnoreLayerCollision(2, 9, true); */

        //if (col.gameObject != null) {
            //Debug.Log("presele jsem pres prvni if");
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyFaction" || col.gameObject.tag == "PlayerFaction") {
                //Debug.Log("TREFIL JSEM TĚ");
                if (col.gameObject.tag == "Enemy")
                {
                col.gameObject.GetComponent<HealthEnemy>().enemyHP -= 40;  //asteroidy
                }
                if (col.gameObject.tag == "EnemyFaction")
                {
                col.gameObject.GetComponent<HealthEnemy>().enemyHP -= 40; //enemyLetadla
                }
                if (col.gameObject.tag == "PlayerFaction")  // player a jeho spojenci
                {
                    if(col.gameObject.layer == 3)
                    {
                    col.gameObject.GetComponent<PlaneHealth>().health -=10;   // player
                    }
                    else 
                    {
                    col.gameObject.GetComponent<HealthEnemy>().enemyHP -=10;  // friendly ai
                    //Debug.Log("PRITEL HRACE BYL TREFEN NEPRITELEM");
                    }
                 
                }
               
               
                //Debug.Log(col.gameObject.GetComponent<HealthEnemy>().enemyHP);
                //Debug.Log(a.enemyHP);
                // Debug.Log(enemyHealth.enemyHP);
                //enemyHealth.enemyHP-=40;
                Destroy(this.gameObject);  //FUNGUJE ODEBIRANI HP NEPRATELUM
            }
            if (col.gameObject.layer == 9)
            {
                Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>());
                //Debug.Log("NABOJ NABOJ MYJE");
            }

            else {
                Destroy(this.gameObject);
            }
        //nemuzu zabit friendly letadlo protoze v ifu playerfaction ubiram zivoty ve skriptu plane health, ale musim ubirat na enemyhealth
        
        //}


    }
}

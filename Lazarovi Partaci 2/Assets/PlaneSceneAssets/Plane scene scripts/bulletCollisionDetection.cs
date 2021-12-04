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

    // Start is called before the first frame update
    void OnTriggerEnter(Collider col) {
        //Physics.IgnoreLayerCollision() PRIDAT PRO IGNOROVANI PLAYERA A NABOJE
        Physics.IgnoreLayerCollision(9, 9, true);
        Physics.IgnoreLayerCollision(0, 9, true);
          Physics.IgnoreLayerCollision(2, 9, true);

        //if (col.gameObject != null) {
            Debug.Log("presele jsem pres prvni if");
            if (col.gameObject.tag == "Enemy") {
                Debug.Log("TREFIL JSEM TĚ");
                col.gameObject.GetComponent<HealthEnemy>().enemyHP -= 40;
                //Debug.Log(col.gameObject.GetComponent<HealthEnemy>().enemyHP);
                //Debug.Log(a.enemyHP);
                // Debug.Log(enemyHealth.enemyHP);
                //enemyHealth.enemyHP-=40;
                Destroy(this.gameObject);  //FUNGUJE ODEBIRANI HP NEPRATELUM
            }
            if (col.gameObject.layer == 9)
            {
                Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>());
                Debug.Log("NABOJ NABOJ MYJE");
            }

            else {
                Destroy(this.gameObject);
            }
        

        //}


    }
}

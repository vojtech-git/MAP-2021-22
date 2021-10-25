using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    // Start is called before the first frame update public int enemyHP;
    public int enemyHP = 100;
    public AudioSource death;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(enemyHP<=0){
            //death.Play();
            Destroy(this.gameObject);
        } //FUNGUJE
    }
}

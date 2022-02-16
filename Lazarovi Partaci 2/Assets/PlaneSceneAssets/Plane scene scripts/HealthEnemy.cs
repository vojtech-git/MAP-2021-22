using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    // Start is called before the first frame update public int enemyHP;
    public int enemyHP = 100;
    public GameObject explosionEffect;
    public AudioSource death;
    public AudioController audiocontroller;
    public int id;
    
    void Start()
    {
        var audio = gameObject.GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(enemyHP<=0){
            //death.Play();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            QuestingManager.onPointGained(GoalType.Kill, id);
            audiocontroller.explosion.Play();
            Destroy(this.gameObject);
        } //FUNGUJE
    }
}

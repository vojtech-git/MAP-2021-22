using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    // Start is called before the first frame update public int enemyHP;
    public int enemyHP = 100;
    public GameObject explosionEffect;
    public AudioController audiocontroller;
    public int id;
    
    [Header("Death effects")]
    public AudioClip deathSound;
    public GameObject spaceAudioSource;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHP<=0)
        {
            //death.Play();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            QuestingManager.onPointGained(GoalType.Kill, id);


            //audiocontroller.explosion.Play(); // na prefabu zadny audiocontroller neni (tenhle radek rozbíjí questy)

            PlayDeathAudio();


            Destroy(this.gameObject);
        } //FUNGUJE
    }

    void PlayDeathAudio()
    {
        AudioSource audioSource = Instantiate(spaceAudioSource, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        audioSource.clip = deathSound;
        audioSource.Play();
    }
}

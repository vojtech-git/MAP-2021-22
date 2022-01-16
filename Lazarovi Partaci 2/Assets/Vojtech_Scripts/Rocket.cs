using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Rocket : Entity
{
    [Header("Stats")]
    public float rocketLifeTime = 15;
    public float rocketDamage = 5;
    public float rocketExplosionRadius = 7;
    public float explosionForce = 700;
    public float rocketSpeed = 0.1f;

    [Header("Explosion prefab and sound")]
    public GameObject explosionEffect;
    public AudioSource exploadingSound;

    Rigidbody rb;
    Coroutine rocketTimer;

    private void Start()
    {
        rocketTimer = StartCoroutine(RocketTimer());
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * rocketSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Die();
    }

    public override void Die()
    {
        if (!isDead)
        {
            isDead = true;
            StopCoroutine(rocketTimer);
            AudioSource.PlayClipAtPoint(exploadingSound.clip, transform.position);
            Instantiate(explosionEffect, transform.position, transform.rotation);
            foreach (Collider target in Physics.OverlapSphere(transform.position, rocketExplosionRadius))
            {
                Entity hitObject = target.GetComponent<Entity>();
                Rigidbody hitObjectRigidbody = target.GetComponent<Rigidbody>();

                if (hitObjectRigidbody != null)
                    hitObjectRigidbody.AddExplosionForce(explosionForce, transform.position, rocketExplosionRadius);
                if (hitObject != null)
                    hitObject.TakeDamage(rocketDamage);
            }
            Destroy(gameObject);
        }
    }

    IEnumerator RocketTimer()
    {
        yield return new WaitForSecondsRealtime(rocketLifeTime);
        Die();
    }
}

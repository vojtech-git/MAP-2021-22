using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Granade : Entity
{
    [Header("Stats")]
    public float granadeLifeTime = 3;
    public float granadeDamage = 5;
    public float granadeExplosionRadius = 7;
    public float explosionForce = 700;

    [Header("Explosion prefab and sound")]
    public GameObject explosionEffect;
    public AudioSource exploadingSound;

    Coroutine granadeTimer;

    void Start()
    {
        granadeTimer = StartCoroutine(GranadeTimer(granadeLifeTime));
    }

    public override void Die()
    {
        if (!isDead)
        {
            //Debug.Log("granade exploading");

            isDead = true;
            AudioSource.PlayClipAtPoint(exploadingSound.clip, transform.position);
            Instantiate(explosionEffect, transform.position, transform.rotation);
            foreach (Collider target in Physics.OverlapSphere(transform.position, granadeExplosionRadius))
            {
                Entity hitObject = target.GetComponent<Entity>();
                Rigidbody hitObjectRigidbody = target.GetComponent<Rigidbody>();

                if (hitObjectRigidbody != null)
                    hitObjectRigidbody.AddExplosionForce(explosionForce, transform.position, granadeExplosionRadius);
                if (hitObject != null)
                    hitObject.TakeDamage(granadeDamage);
            }
            Destroy(gameObject);
        }
    }

    IEnumerator GranadeTimer(float lifetime)
    {
        yield return new WaitForSecondsRealtime(lifetime);
        Die();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float health;

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        // edit healthbar?

        if (health <= 0)
        {
            Die();
        }        
    }
    public virtual void AddHealth(float damage)
    {
        health -= damage;

        // edit healthbar?

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}

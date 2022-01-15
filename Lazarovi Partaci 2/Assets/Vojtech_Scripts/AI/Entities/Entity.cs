using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth;
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    /// <summary>
    /// Health by se nemìlo dát mìnit rovnou mìní se pøes TakeDamage() a AddHealth() metody aby tam mohla byt zaøízená logika toho co se deje kdyz se mìní životy (zmìna UI)
    /// </summary>
    protected float health;
    public float Health 
    { 
        get { return health; }
        protected set 
        {
            if (value > maxHealth)
            {
                value = maxHealth;
            }
            health = value;
        } 
    }

    public bool isDead = false;

    public virtual void TakeDamage(float howMuch)
    {
        Health -= howMuch;

        if (Health <= 0)
            Die();
    }
    public virtual void AddHealth(float howMuch)
    {
        Health += howMuch;

        if (Health <= 0)
            Die();
    }

    public virtual void Die()
    {
        isDead = true;
        Destroy(this.gameObject);
    }
}

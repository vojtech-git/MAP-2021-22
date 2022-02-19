using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public Slider healthBar;

    [SerializeField]
    protected float maxHealth = 100;    
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
            // jestli je hodnota vìtší než maxHealth
            if (value > maxHealth)
            {
                // setni hodnotu na maxHealth
                value = maxHealth;
            }

            // nastav hodnotu do private var
            health = value;

            //pokud má entita healthBar updatni healthBar
            if (healthBar != null)
            {
                healthBar.value = health;
            }

            if (health <= 0)
            {
                Die();
            }
        } 
    }
    [HideInInspector]
    public bool isDead = false;

    private void Start()
    {
        Health = MaxHealth;
    }

    public virtual void TakeDamage(float howMuch)
    {
        if (enabled)
        {
            Health -= howMuch; 
        }
    }
    public virtual void AddHealth(float howMuch)
    {
        if (enabled)
        {
            Health += howMuch;
        }
    }

    public virtual void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}

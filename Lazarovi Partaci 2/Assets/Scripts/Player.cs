using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float currentSpeed = 1f;
    public CharacterController controller;
    public float movementSpeed = 6f;
    public float sprintSpeed = 18f;
    public float crouchSpeed = 3f;
    public float jumpHeight = 3f;
    public int jumpStamina = 5;
    public float gravity = -9.81f;
    public bool isGrounded;
    public Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Health")]
    public Slider healthBar;
    public Volume healthVolume;
    public int maxHealth = 100;
    public int currentHealth;
    public bool healthRegenerate = false;
    public WaitForSeconds healthTick = new WaitForSeconds(0.1f);
    private Coroutine healthWait;

    [Header("Stamina")]
    public Slider staminaBar;
    public int maxStamina = 100;
    public int currentStamina;
    public WaitForSeconds staminaTick = new WaitForSeconds(0.1f);
    private Coroutine staminaWait;
    
    [Header("Money")]
    public Text moneyText;
    public int currentMoney;
    
    void Start()
    {
        //Movement
        currentSpeed = movementSpeed;

        //Money
        moneyText.text = currentMoney.ToString() + " ¤";

        //Health
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        
        //Stamina
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;

        
    }
    void Update()
    {
        //PlaceHolders
        #region Placeholders
        if(Input.GetKeyDown(KeyCode.B))
        {
            UseHealth(-10);
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            UseHealth(10);
        }
        #endregion

        #region Healt
        healthBar.value = currentHealth;
        #endregion

        #region Movement
        //Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal1");
        float z = Input.GetAxis("Vertical1");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);
        currentSpeed = movementSpeed;
        if(isGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                if(currentStamina - jumpStamina < 0)
                {
                    
                }
                else
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    UseStamina(jumpStamina);
                }
            }

            if(Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = sprintSpeed;
            }
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = crouchSpeed;
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        #endregion
        #region UI
        if(Input.GetKey(KeyCode.Tab))
        {

        }
        #endregion
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UseMoney(int amount)
    {
        if(currentMoney - amount >= 0)
        {
            currentMoney -= amount;
            moneyText.text = currentMoney.ToString() + " ¤";
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UseStamina(int amount)
    {
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if(staminaWait != null)
            {
                StopCoroutine(staminaWait);
            }
            
            staminaWait = StartCoroutine(RegenStamina());
        }
    }
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(3);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return staminaTick;
        }
        staminaWait = null;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UseHealth(int amount)
    {
        
        // + odečítá
        if(amount > 0)
        {
            if(currentHealth - amount >= 0)
            {
                currentHealth -= amount;
                healthBar.value = currentHealth;
            }
        }
        //- přičítá
        if(amount < 0)
        {
            if(currentHealth - amount < maxHealth)
            {
                currentHealth -= amount;
                healthBar.value = currentHealth;
            }
            else if(currentHealth - amount >= maxHealth)
            {
                currentHealth = maxHealth;
                healthBar.value = maxHealth;
            }
            else
            {

            }
        }
        if(healthWait != null)
        {
            StopCoroutine(healthWait);
        }
        if(healthRegenerate == true)
        {
            healthWait = StartCoroutine(RegenHealth());
        }

    }
    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(3);

        while(currentHealth < maxHealth)
        {
            currentHealth += maxHealth / 100;
            healthBar.value = currentHealth;
            yield return healthTick;
        }
        healthWait = null;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

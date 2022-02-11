using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : Entity
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
    public Volume healthVolume;
    public bool healthRegenerate = false;
    public WaitForSeconds healthTick = new WaitForSeconds(0.1f);
    private Coroutine healthWait;
    public GameObject defeatScreen;

    [Header("Stamina")]
    public Slider staminaBar_Left;
    public Slider staminaBar_Right;
    public int maxStamina = 100;
    public int currentStamina;
    public WaitForSeconds staminaTick = new WaitForSeconds(0.01f);
    private Coroutine staminaWait;
    public int sprintStaminaCost = 1;
    public WaitForSeconds staminaSprintTick = new WaitForSeconds(0.1f);
    private Coroutine staminaCostWait;

    private bool enterSprint = true;

    [Header("Money")]
    public Text moneyText;
    public Text moneyText_Back;
    public int currentMoney;

    void Start()
    {
        //Movement
        currentSpeed = movementSpeed;

        //Money
        moneyText.text = currentMoney.ToString() + " ¤";
        moneyText_Back.text = moneyText.text;

        //Health
        Health = MaxHealth;
        healthBar.maxValue = MaxHealth;
        healthBar.value = MaxHealth;

        //Stamina
        currentStamina = maxStamina;
        staminaBar_Left.maxValue = maxStamina;
        staminaBar_Left.value = maxStamina;
        staminaBar_Right.maxValue = maxStamina;
        staminaBar_Left.value = maxStamina;

        GetComponent<CharacterController>().enabled = false;
        transform.position = SaveData.loadPosition;
        GetComponent<CharacterController>().enabled = true;
    }

    void Update()
    {
        //PlaceHolders
        #region Placeholders
        if(Input.GetKeyDown(KeyCode.B))
        {
            TakeDamage(10);
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            AddHealth(10);
        }
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
                if (currentStamina > 0 && enterSprint == true)
                {
                    SprintStaminaUse();
                }
            }
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = crouchSpeed;
            controller.height = 0.9f;
        }
        else
        {
            controller.height = 1.8f;
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        #endregion
        #region stamina
        staminaBar_Right.value = currentStamina;
        #endregion
        #region money
        moneyText_Back.text = moneyText.text;
        #endregion
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool UseMoney(int amount)
    {
        if(currentMoney - amount >= 0)
        {
            currentMoney -= amount;
            moneyText.text = currentMoney.ToString() + " ¤";
            return true;
        }
        else
        {
            return false;
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UseStamina(int amount)
    {
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar_Left.value = currentStamina;

            if(staminaWait != null)
            {
                StopCoroutine(staminaWait);
            }
            
            staminaWait = StartCoroutine(RegenStamina());
        }
    }

    private void SprintStaminaUse()
    {
        if (currentStamina > 0)
        {
            staminaBar_Left.value = currentStamina;

            if (staminaCostWait != null)
            {
                StopCoroutine(staminaCostWait);
            }

            staminaCostWait = StartCoroutine(UseSprintStamina());
        }
    }
    private IEnumerator UseSprintStamina()
    {
        enterSprint = false;

        while (Input.GetKey(KeyCode.LeftShift))
        {
            currentStamina -= sprintStaminaCost;
            staminaBar_Left.value = currentStamina;
            yield return staminaSprintTick;
        }
        enterSprint = true;
        staminaCostWait = null;
        if (staminaWait != null)
        {
            StopCoroutine(staminaWait);
        }

        staminaWait = StartCoroutine(RegenStamina());
    }
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(3);

        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar_Left.value = currentStamina;
            yield return staminaTick;
        }
        staminaWait = null;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override void TakeDamage(float howMuch)
    {
        if (healthWait == null)
        {
            healthWait = StartCoroutine(RegenHealth());
        }
        else if (healthWait != null)
        {
            StopCoroutine(healthWait);
            healthWait = StartCoroutine(RegenHealth());
        }

        base.TakeDamage(howMuch);
    }
    public override void AddHealth(float howMuch)
    {
        base.AddHealth(howMuch);
    }

    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(3);

        while (Health < MaxHealth)
        {
            AddHealth(MaxHealth / 100);
            yield return healthTick;
        }

        healthWait = null;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public override void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("player umřel");
            Cursor.lockState = CursorLockMode.None; //defeat screen
            DefeatScreen.jsiDead = true;
            //Time.timeScale = 0f;
            // Debug.Log(Time.timeScale);
            defeatScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
    }
}

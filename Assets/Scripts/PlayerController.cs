using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; // Singleton instance of PlayerController
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    private Animator animator; // Animator for player animations 
    private bool isBoosting = false; // Flag to check if player is boosting
    [SerializeField] private GameObject boomPrefab; // Prefab for explosion effect
    [SerializeField] private float speed = 5f; // Speed of the player
    [SerializeField] public float boost = 1f;
    [SerializeField] private float boostSpeed = 5f; // Speed when boosting
    [SerializeField] private float energy; // Player's energy level
    [SerializeField] private float energyMax = 100f; // Maximum energy level
    [SerializeField] private float energyUsedPerBoost = 7f; // Energy used per boost
    [SerializeField] private float energyRegenRate = 1f; // Rate at which energy regenerates per second
    [SerializeField] private float health; // Player's health level
    [SerializeField] private float healthMax = 100f; // Maximum health level
    [SerializeField] private GameObject bulletPrefabs; // Prefab for player bullet
    [SerializeField] private Transform firepos; // Position where bullets are fired from

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assign the singleton instance
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Initialize the animator component
        energy = energyMax; // Set initial energy to maximum
        health = healthMax; // Set initial health to maximum
        if (UIController.Instance != null) // Check if UIController instance exists
        {
            UIController.Instance.UpdateEnergyUI(energy, energyMax); // Update the energy UI
            UIController.Instance.UpdateHealthUI(health, healthMax); // Update the health UI
        }
        else
        {
            Debug.LogWarning("UIController instance not found. Energy UI will not be updated.");
        }
        
    }


    void Update()
    {
        PlayerMovement();
        Fire(); // Call the Fire method to handle shooting
    }
    private void PlayerMovement()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");
        animator.SetFloat("moveX", directionX); // Set horizontal movement for animations
        animator.SetFloat("moveY", directionY); // Set vertical movement for animations
        playerDirection = new Vector2(directionX, directionY).normalized;
        rb.linearVelocity = new Vector2(playerDirection.x * speed, playerDirection.y * speed); // Adjust speed as needed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(energy > 1f) 
            {
            EnterBoost(); // Call method to enter boost state
            }
           
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            ExitBoost(); // Call method to exit boost state
        }
        if (isBoosting) 
        {
            if (energy > 0f) 
            { 
                energy -= energyUsedPerBoost * Time.deltaTime; 
            }
            else
            {
                ExitBoost(); // Exit boost if energy is too low
            }
        }
        else if (energy < energyMax) 
        {
            energy += energyRegenRate * Time.deltaTime; // Regenerate energy over time
        }
        energy = Mathf.Clamp(energy, 0f, energyMax); // Ensure energy does not exceed max or go below zero
        if (UIController.Instance != null) // Check if UIController instance exists
        {
            UIController.Instance.UpdateEnergyUI(energy, energyMax); // Update the energy UI
        }
        else
        {
            Debug.LogWarning("UIController instance not found. Energy UI will not be updated.");
        }
    }
    private void EnterBoost()
    {
        boost = boostSpeed; // Set boost speed
        animator.SetBool("isBoosting", true);
        isBoosting = true; // Set the boosting flag
        AudioManager.Instance.PlayFireSound(); // Play fire sound effect when boosting
    }
    private void ExitBoost()
    {
        boost = 1f; // Reset boost speed to normal
        animator.SetBool("isBoosting", false); // Reset the boosting animation
        isBoosting = false;
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount; // Reduce health by the damage amount
        health = Mathf.Clamp(health, 0f, healthMax); // Ensure health does not exceed max or go below zero
        AudioManager.Instance.PlayHitSound(); // Play damage sound effect
        if (health <= 0f) // Check if health is zero or below
        {
            Die(); // Call method to handle player death
            boost = 0f; // Reset boost speed to normal
        }
        if (UIController.Instance != null) // Check if UIController instance exists
        {
            UIController.Instance.UpdateHealthUI(health, healthMax); // Update the health UI
        }
        else
        {
            Debug.LogWarning("UIController instance not found. Health UI will not be updated.");
        }
    }
    private void Die()
    {
       
        Destroy(gameObject); // Destroy the player object
        GameObject boom = Instantiate(boomPrefab, transform.position, Quaternion.identity); // Instantiate explosion effect at player's position
        AudioManager.Instance.PlayIceSound(); // Play ice sound effect
        if (boom != null) // Check if the explosion prefab was instantiated successfully
        {
            Destroy(boom, 0.4f); // Destroy the explosion effect after 0.4 seconds
        }
        else
        {
            Debug.LogWarning("Boom prefab not assigned or not found."); // Log warning if boom prefab is not assigned
        }
        if(GameManager.Instance != null) 
        {
        GameManager.Instance.GameOver(); // Call the Menu method from GameManager to return to the main menu
        }
        else
        {
            Debug.LogWarning("GameManager instance not found. Cannot return to menu."); // Log warning if GameManager instance is not found
        }
    }
    private void Fire() 
    {
        if (bulletPrefabs != null && firepos != null) // Check if bullet prefab and fire position are assigned
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject bullet = Instantiate(bulletPrefabs, firepos.position, Quaternion.identity); // Instantiate bullet at fire position
            }
           
        }
        else
        {
            Debug.LogWarning("Bullet prefab or fire position not assigned. Cannot fire."); // Log warning if bullet prefab or fire position is not assigned
        }
    }
   
}

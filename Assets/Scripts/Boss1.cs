using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private float speedX;
    private float speedY;
    private bool isCharging;
    private float switchTimer;
    private float switchInterval;
    private Animator animator; // Animator to control animations
    public static Boss1 instance; // Singleton instance for easy access
    [SerializeField] private float maxHealth = 100f; // Maximum health of the boss
    [SerializeField] private float currentHealth; // Current health of the boss
    [SerializeField] private GameObject deadPrefabs; // Prefab to instantiate when the boss is destroyed
    private bool isaDead = false;
    void Start()
    {
        if (instance == null) // Check if the instance is not already set
        {
            instance = this; // Set the singleton instance
        }
        currentHealth = maxHealth; // Initialize current health to maximum health
        PlayBossSound();
        animator = GetComponent<Animator>(); // Get the Animator component
        EnterCharegeMode(); // Initialize the boss in charge mode
    }


    void Update()
    {
        MoveBoss1(); // Call the method to move the boss
    }
    private void MoveBoss1()
    {
        if (switchTimer > 0)
        {
            switchTimer -= Time.deltaTime; // Decrease the switch timer
        }
        else
        {
            if (isCharging) // Check if the boss is in charge mode
            {
                EnterPatrolState(); // Switch to patrol state
                isCharging = false; // Set charging state to false
            }
            else
            {
                EnterCharegeMode(); // Switch to charge mode
                isCharging = true; // Set charging state to true
            }
        }
        if (transform.position.y > 3 || transform.position.y < -3) // Check if the boss is within vertical bounds
        {
            speedY *= -1; // Reverse vertical speed if out of bounds
        }
        float moveX = (speedX * PlayerController.instance.boost) * Time.deltaTime; // Calculate movement based on player's boost
        float moveY = speedY * Time.deltaTime; // Calculate vertical movement based on player's boost
        transform.position += new Vector3(-moveX, moveY); // Move the asteroid horizontally
        if (transform.position.x < -12f) // Check if the asteroid has moved off-screen
        {
            Destroy(gameObject); // Destroy the asteroid if it goes off-screen
        }

    }
    void EnterCharegeMode()
    {
        speedX = Random.Range(3f, 4f); // Set a random horizontal speed for charge mode
        speedY = 0f; // Set vertical speed to 0 for charge mode
        animator.SetBool("isCharging", true); // Trigger the charge animation
        switchInterval = Random.Range(1f, 2f); // Set a random interval for switching states
        switchTimer = switchInterval; // Initialize the switch timer
    }
    void EnterPatrolState()
    {
        speedX = 0f; // Set horizontal speed to 0 for patrol state
        speedY = Random.Range(-3f, 3f); // Set a random vertical speed for patrol state
        animator.SetBool("isCharging", false); // Trigger the patrol animation
        switchInterval = Random.Range(4f, 6f); // Set a random interval for switching states
        switchTimer = switchInterval;
    }
    public void TakeDamage(float damageAmount)
    {
        if (isaDead) return;
        currentHealth -= damageAmount; // Reduce the boss's health by the damage amount
        if (currentHealth <= 0f) // Check if the boss's health is zero or below
        {
            isaDead = true; // Mark as killed by player so win flow can run in OnDestroy
            Destroy(gameObject); // Destroy the boss if its health is depleted
            
        }
    }
    private void PlayBossSound()
    {
        AudioManager.Instance.PlayBossSound();
    }
    private void OnDestroy()
    {
        if (isaDead)
        {
            if (deadPrefabs != null) // Check if the dead prefab is assigned
            {
                GameObject explo = Instantiate(deadPrefabs, transform.position, Quaternion.identity); // Instantiate the dead prefab at the boss's position
                Destroy(explo, 0.1f); // Call the OnDestroy method on the instantiated prefab
                GameManager.Instance.GameWin(); // Call the GameWin method in GameManager to handle game win logic
            }
        }

    }
}

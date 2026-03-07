using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // SpriteRenderer to change asteroid color
    private Rigidbody2D rb; // Rigidbody2D for physics interactions
    [SerializeField] private float speed = 1f; // Speed of the asteroid
    [SerializeField] private Sprite[] sprites; // Array of asteroid sprites for different colors
    [SerializeField] private GameObject explosionPrefab; // Prefab for explosion effect when asteroid is destroyed
    [SerializeField] private float maxHealth = 100f; // Maximum health of the asteroid
    private float currentHealth; // Current health of the asteroid
 
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        currentHealth = maxHealth; // Initialize current health to maximum health
        AsteroidFly(); // Call the method to set the asteroid's sprite and initial velocity
    }

   
    void Update()
    {
        MoveAsteroid(); // Call the method to move the asteroid
    }
    private void MoveAsteroid()
    {
       float moveX = (speed * PlayerController.instance.boost) * Time.deltaTime; // Calculate movement based on player's boost
       transform.position += new Vector3(-moveX, 0f); // Move the asteroid horizontally
         if (transform.position.x < -12f) // Check if the asteroid has moved off-screen
         {
              Destroy(gameObject); // Destroy the asteroid if it goes off-screen
         }
        
    }
    private void AsteroidFly() 
    {
        if (sprites.Length > 0)
        {
            int randomIndex = Random.Range(0, sprites.Length); // Get a random index for the sprite array
            spriteRenderer.sprite = sprites[randomIndex]; // Set the sprite to a random one from the array
        }
        else
        {
            Debug.LogWarning("No asteroid sprites assigned in the inspector."); // Log warning if no sprites are assigned
        }
        float pushX = Random.Range(-1f, 0); // Random horizontal push force
        float pushY = Random.Range(-1f, 1f); // Random vertical push force
        rb.linearVelocity = new Vector2(pushX, pushY).normalized * Random.Range(1f, 1f); // Set the asteroid's velocity with a random force
    }
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; // Reduce the asteroid's health by the damage amount
        if (currentHealth <= 0f) // Check if the asteroid's health is zero or below
        {
            DestroyAsteroid(); // Call the method to destroy the asteroid
            GameManager.Instance.sumCount += 1f; // Update the total count of enemies in GameManager
            UIController.Instance.UpdateBosSlider(GameManager.Instance.sumCount, GameManager.Instance.maxSumCount);
        }
    }
    private void DestroyAsteroid()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Create explosion effect
        AudioManager.Instance.PlayExploSound(); // Play explosion sound effect
        Destroy(explosion, 0.2f); // Destroy the explosion effect after 0.4 seconds
        Destroy(gameObject); // Destroy the asteroid
    }
}

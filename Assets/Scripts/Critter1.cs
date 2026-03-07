using UnityEngine;

public class Critter1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    private float speed;
    private Vector3 targetPosition;
    private float moveTimer;
    private float moveInterval;
    private Quaternion targetRotation;
    [SerializeField]private float maxHealth = 100f; // Maximum health of the critter
    private float currentHealth; // Current health of the critter
    [SerializeField] private GameObject exploPrefabs; // Prefab for explosion effect when critter is destroyed
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth; // Initialize current health to maximum health
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
        speed = Random.Range(2f, 5f);
        moveInterval = Random.Range(1f, 2f);
        moveTimer = moveInterval;
        GenerateRandomPosition();
    }

    
    void Update()
    {
        CritterMove();
    }
    private void CritterMove() 
    {
        if (moveTimer <= 0f) 
        {
            GenerateRandomPosition();
            moveInterval = Random.Range(1f, 2f);
            moveTimer = moveInterval;
        }
        else 
        {
            moveTimer -= Time.deltaTime;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        Vector3 relativePos = targetPosition - transform.position;
        if (relativePos != Vector3.zero) 
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 1080f);
        }
        float moveX = (speed * PlayerController.instance.boost) * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if (transform.position.x < -12f) 
        {
            Destroy(gameObject); // Destroy the critter if it goes off-screen
        }

    }
    private void GenerateRandomPosition()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-9f, 9f);
        targetPosition = new Vector2(x, y);
    }
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; // Reduce the critter's health by the damage amount
        if (currentHealth <= 0f) // Check if the critter's health is zero or below
        {
            DestroyCritter(); // Call the method to destroy the critter
            GameManager.Instance.sumCount += 2f; // Update the total count of enemies in GameManager
            UIController.Instance.UpdateBosSlider(GameManager.Instance.sumCount, GameManager.Instance.maxSumCount);
        }
    }
   private void DestroyCritter()
    {
        GameObject explosion = Instantiate(exploPrefabs, transform.position, Quaternion.identity); // Create explosion effect
        GenerateRandomPosition(); // Generate a new random position for the explosion
        AudioManager.Instance.PlayExploSound(); // Play explosion sound effect
        Destroy(explosion, 0.4f); // Destroy the explosion effect after 0.2 seconds
        Destroy(gameObject); // Destroy the critter
    }
}

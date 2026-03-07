using UnityEngine;

public class Whale : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // Speed of the whale
    [SerializeField] private GameObject explosionPrefab; // Prefab for explosion effect when whale is destroyed
    [SerializeField] private float maxHealth = 100f; // Maximum health of the whale
    private float currentHealth; // Current health of the whale
 
    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
        PlayWhaleSound(); // Play whale sound effect when the whale is created
    }

    // Update is called once per frame
    void Update()
    {
        MoveWhale(); // Call the method to move the whale
       
    }
    private void MoveWhale()
    {
        float moveX = (speed * PlayerController.instance.boost) * Time.deltaTime; // Calculate movement based on player's boost
        transform.position += new Vector3(-moveX, 0f); // Move the whale horizontally
        if (transform.position.x < -12f) // Check if the whale has moved off-screen
        {
            Destroy(gameObject); // Destroy the whale if it goes off-screen
        }
    }
    private void PlayWhaleSound()
    {
        AudioManager.Instance.PlayWhaleSound(); // Play whale sound effect
    }
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; // Reduce the asteroid's health by the damage amount
        if (currentHealth <= 0f) // Check if the asteroid's health is zero or below
        {
            DestroyWhale(); // Call the method to destroy the asteroid
            GameManager.Instance.sumCount += 3f; // Update the total count of enemies in GameManager
            UIController.Instance.UpdateBosSlider(GameManager.Instance.sumCount, GameManager.Instance.maxSumCount);
        }
    }
    private void DestroyWhale()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Create explosion effect
        AudioManager.Instance.PlayExploSound(); // Play explosion sound effect
        Destroy(explosion, 0.2f); // Destroy the explosion effect after 0.4 seconds
        Destroy(gameObject); // Destroy the asteroid
    }

}

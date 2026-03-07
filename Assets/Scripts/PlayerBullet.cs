using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Speed of the bullet
    [SerializeField] private GameObject explosionPrefab; // Prefab for explosion effect
    void Start()
    {
        PlayBulletSound(); // Play bullet sound effect when the bullet is created
    }
    void Update()
    {
        MoveBullet(); // Call the method to move the bullet
    }
    private void MoveBullet()
    {
        float moveX = (speed * PlayerController.instance.boost) * Time.deltaTime; // Calculate movement based on player's boost
        transform.position += new Vector3(moveX, 0f); // Move the bullet horizontally
        if (transform.position.x > 12f) // Check if the bullet has moved off-screen
        {
            Destroy(gameObject); // Destroy the bullet if it goes off-screen
        }
    }
    private void PlayBulletSound()
    {
        AudioManager.Instance.PlayBulletFireSound(); // Play bullet sound effect
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) // Check if collided with an asteroid
        {

            Destroy(gameObject); // Destroy the bullet
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Create explosion effect
            AudioManager.Instance.PlayExploSound(); // Play explosion sound effect
            Destroy(explosion, 0.4f); // Destroy the explosion effect after 0.4 seconds
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>(); // Get the Asteroid component from the collided object
            if (asteroid != null) // Check if the asteroid component exists
            {
                asteroid.TakeDamage(10f); // Call the TakeDamage method on the asteroid with a damage value
            }
           
            Whale whale = collision.gameObject.GetComponent<Whale>(); // Get the Whale component from the collided object
            if (whale != null) // Check if the whale component exists
            {
                whale.TakeDamage(10f); // Call the TakeDamage method on the whale with a damage value
            }
            PinkWhale pinkWhale = collision.gameObject.GetComponent<PinkWhale>(); // Get the PinkWhale component from the collided object
            if (pinkWhale != null) // Check if the pink whale component exists
            {
                pinkWhale.TakeDamage(10f); // Call the TakeDamage method on the pink whale with a damage value
            }
            Critter1 critter = collision.gameObject.GetComponent<Critter1>(); // Get the Critter component from the collided object
            if (critter != null) // Check if the critter component exists
            {
                critter.TakeDamage(10f); // Call the TakeDamage method on the critter with a damage value
            }
            

        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject); // Destroy the bullet
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Create explosion effect
            AudioManager.Instance.PlayExploSound(); // Play explosion sound effect
            Destroy(explosion, 0.4f); // Destroy the explosion effect after 0.4 seconds
            Boss1.instance.TakeDamage(5f);
        }
      
       
    }
   
}

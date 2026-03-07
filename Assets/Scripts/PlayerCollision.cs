using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) // Check if collided with an asteroid
        {
            PlayerController.instance.TakeDamage(10f); // Call method to take damage
        }
        if (collision.gameObject.CompareTag("Critter")) // Check if collided with a whale
        {
            PlayerController.instance.TakeDamage(5f); // Call method to take damage
        }
        if (collision.gameObject.CompareTag("Boss")) // Check if collided with the boss
        {
            AudioManager.Instance.PlayHitByBossSound(); // Play boss hit sound effect
            PlayerController.instance.TakeDamage(20f); // Call method to take damage
        }
    }
}

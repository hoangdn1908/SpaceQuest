using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance
    [SerializeField] private AudioSource backgroundMusic; // Background music audio source
    [SerializeField] private AudioSource soundEffects; // Sound effects audio source
    [SerializeField] private AudioClip iceClip;
    [SerializeField] private AudioClip unPauseClip; // Audio clip for unpause sound effect
    [SerializeField] private AudioClip pauseClip; // Audio clip for pause sound effect
    [SerializeField] private AudioClip whaleClip; // Audio clip for whale sound effect
    [SerializeField] private AudioClip hitClip; // Audio clip for hit sound effect
    [SerializeField] private AudioClip fireClip; // Audio clip for fire sound effect
    [SerializeField] private AudioClip bulletFireClip; // Audio clip for bullet fire sound effect
    [SerializeField] private AudioClip exploClip; // Audio clip for explosion sound effect
    [SerializeField] private AudioClip bossClip;
    [SerializeField] private AudioClip hitByBoss; // Audio clip for hit by boss sound effect
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the singleton instance
           
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
    public void PlayBackgroundMusic()
    {
        if (!backgroundMusic.isPlaying) // Check if the background music is not already playing
        {
            backgroundMusic.Play(); // Play the background music
        }
    }
    public void PlayIceSound()
    {
        soundEffects.PlayOneShot(iceClip); // Play the ice sound effect
    }
    public void PlayUnPauseSound()
    {
        soundEffects.PlayOneShot(unPauseClip); // Play the unpause sound effect
    }
    public void PlayPauseSound()
    {
        soundEffects.PlayOneShot(pauseClip); // Play the pause sound effect
    }
    public void PlayWhaleSound()
    {
        soundEffects.PlayOneShot(whaleClip); // Play the whale sound effect
    }
    public void PlayHitSound()
    {
        soundEffects.PlayOneShot(hitClip); // Play the hit sound effect
    }
    public void PlayFireSound()
    {
        soundEffects.PlayOneShot(fireClip); // Play the fire sound effect
    }
    public void PlayBulletFireSound()
    {
        soundEffects.PlayOneShot(bulletFireClip); // Play the bullet fire sound effect
    }
    public void PlayExploSound()
    {
        soundEffects.PlayOneShot(exploClip); // Play the explosion sound effect
    }
    public void PlayBossSound() 
    {
        soundEffects.PlayOneShot(bossClip);
    }
    public void PlayHitByBossSound() 
    {
        soundEffects.PlayOneShot(hitByBoss); // Play the hit by boss sound effect
    }
}

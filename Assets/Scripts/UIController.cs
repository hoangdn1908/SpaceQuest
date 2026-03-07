using UnityEngine;
using UnityEngine.UI; // Import UI namespace for UI elements
using TMPro; // Import TextMeshPro namespace for text elements
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance; // Singleton instance
    public Slider energySlider; // Slider to display energy level
    public TextMeshProUGUI energyText; // Text to display energy level
    public Slider healthSlider; // Slider to display health level 
    public TextMeshProUGUI healthText; // Text to display health level
    public GameObject pauseUI; // GameObject for pause UI
    public Slider bossSlider; // Slider to display boss health level
    void Awake()
    {
        // Ensure only one instance of UIController exists
        if (Instance == null)
        {
            Instance = this;

        }
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainMenu")
        {
            Destroy(gameObject); // Hủy UIController nếu đang ở menu
            return;
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1") // Hoặc tên scene gameplay của bạn
        {
            if (energySlider != null) energySlider.gameObject.SetActive(true);
            if (energyText != null) energyText.gameObject.SetActive(true);
            if (healthSlider != null) healthSlider.gameObject.SetActive(true);
            if (healthText != null) healthText.gameObject.SetActive(true);
        }
    }

    public void UpdateEnergyUI(float currentEnergy, float maxEnergy)
    {
        if (energySlider != null && energyText != null)
        {
            energySlider.value = currentEnergy / maxEnergy; // Update slider value based on current energy
            energyText.text = $"{currentEnergy:F0} / {maxEnergy:F0}"; // Update text to show current and max energy
        }
        else
        {
            Debug.LogWarning("Energy UI elements are not assigned in the inspector.");
        }
    }
    public void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        if (healthSlider != null && healthText != null)
        {
            healthSlider.value = currentHealth / maxHealth; // Update slider value based on current health
            healthText.text = $"{currentHealth:F0} / {maxHealth:F0}"; // Update text to show current and max health
        }
        else
        {
            Debug.LogWarning("Health UI elements are not assigned in the inspector.");
        }
    }
    public void UpdateBosSlider(float current, float max)
    {
        if (bossSlider != null)
        {
            bossSlider.value = current / max; // Update boss health slider value
        }
        else
        {
            Debug.LogWarning("Boss health slider is not assigned in the inspector.");
        }
    }
}

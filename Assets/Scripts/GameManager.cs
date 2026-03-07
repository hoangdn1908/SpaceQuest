using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Import UI namespace for UI elements
using System.Collections; // Import for IEnumerator

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float maxSumCount = 20f; // Maximum total count of enemies
    public float sumCount; // Total count of enemies
    public GameObject BossPrefabs; // Prefab for the boss
    private bool hasSpawnedBoss = false;

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
    void Start()
    {
        UIController.Instance.pauseUI.SetActive(false); // Hide pause UI at the start
        UIController.Instance.UpdateBosSlider(sumCount, maxSumCount); // Initialize boss slider with total count
        Time.timeScale = 1f;
    }
    void Update()
    {
        Pause(); // Check for pause input every frame
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SpawnBoss(); // Check if boss should be spawned in Level1
        }
    }
    public void Pause()
    {
        if (SceneManager.GetActiveScene().name != "Level1") return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!UIController.Instance.pauseUI.activeSelf)
            {
                UIController.Instance.pauseUI.SetActive(true);
                AudioManager.Instance.PlayPauseSound(); // Play pause sound effect
                Time.timeScale = 0f;
            }
            else
            {
                UIController.Instance.pauseUI.SetActive(false);
                AudioManager.Instance.PlayUnPauseSound(); // Play unpause sound effect
                Time.timeScale = 1f;
            }
        }

    }
    public void Resume()
    {
        UIController.Instance.pauseUI.SetActive(false); // Hide pause UI
        AudioManager.Instance.PlayUnPauseSound(); // Play unpause sound effect
        Time.timeScale = 1f; // Resume the game by setting time scale to 1
    }
   
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        UIController.Instance.pauseUI.SetActive(false);
        if (UIController.Instance.energySlider != null)
            UIController.Instance.energySlider.gameObject.SetActive(false);

        if (UIController.Instance.energyText != null)
            UIController.Instance.energyText.gameObject.SetActive(false);

        if (UIController.Instance.healthSlider != null)
            UIController.Instance.healthSlider.gameObject.SetActive(false);

        if (UIController.Instance.healthText != null)
            UIController.Instance.healthText.gameObject.SetActive(false);

        if (UIController.Instance.bossSlider != null)
            UIController.Instance.bossSlider.gameObject.SetActive(false);
    }
    public void GameOver()
    {
        StartCoroutine(DelayGameOver());
    }

    private IEnumerator DelayGameOver()
    {
        Time.timeScale = 1f; // Đảm bảo không bị pause
        yield return new WaitForSeconds(1f); // Chờ 1 giây (đủ cho animation/hiệu ứng)

        SceneManager.LoadScene("GameOver");

        if (UIController.Instance.pauseUI != null)
            UIController.Instance.pauseUI.SetActive(false);

        if (UIController.Instance.energySlider != null)
            UIController.Instance.energySlider.gameObject.SetActive(false);

        if (UIController.Instance.energyText != null)
            UIController.Instance.energyText.gameObject.SetActive(false);

        if (UIController.Instance.healthSlider != null)
            UIController.Instance.healthSlider.gameObject.SetActive(false);

        if (UIController.Instance.healthText != null)
            UIController.Instance.healthText.gameObject.SetActive(false);

        if (UIController.Instance.bossSlider != null)
            UIController.Instance.bossSlider.gameObject.SetActive(false);
    }
    private IEnumerator DelayGameWin()
    {
        Time.timeScale = 1f; // Đảm bảo không bị pause
        yield return new WaitForSeconds(1f); // Chờ 1 giây (đủ cho animation/hiệu ứng)

        SceneManager.LoadScene("MissionComplete");

        if (UIController.Instance.pauseUI != null)
            UIController.Instance.pauseUI.SetActive(false);

        if (UIController.Instance.energySlider != null)
            UIController.Instance.energySlider.gameObject.SetActive(false);

        if (UIController.Instance.energyText != null)
            UIController.Instance.energyText.gameObject.SetActive(false);

        if (UIController.Instance.healthSlider != null)
            UIController.Instance.healthSlider.gameObject.SetActive(false);

        if (UIController.Instance.healthText != null)
            UIController.Instance.healthText.gameObject.SetActive(false);

        if (UIController.Instance.bossSlider != null)
            UIController.Instance.bossSlider.gameObject.SetActive(false);
    }
    public void GameWin()
    {
       StartCoroutine(DelayGameWin());
    }
    public void SpawnBoss()
    {
        if (!hasSpawnedBoss && sumCount >= maxSumCount)
        {
            hasSpawnedBoss = true; // Đánh dấu đã triệu hồi boss
            sumCount = 0f; // Reset sumCount
            UIController.Instance.UpdateBosSlider(sumCount, maxSumCount); // Reset thanh slider

            GameObject boss = Instantiate(BossPrefabs, new Vector3(15f, 0f, 0f), Quaternion.Euler(0, 0, -90));
        }
        else 
        {
        hasSpawnedBoss = false; // Đặt lại trạng thái đã triệu hồi boss nếu chưa đủ điều kiện
        }
    }



}

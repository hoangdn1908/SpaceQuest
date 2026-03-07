using UnityEngine;
using UnityEngine.UI; // Import UI namespace for UI elements
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start() 
    {
    Time.timeScale = 1f;
    }
    public void Play() 
    {
        SceneManager.LoadScene("Level1");
    }
    public void Quit() 
    {
        Application.Quit();
    }
    public void LoadMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}

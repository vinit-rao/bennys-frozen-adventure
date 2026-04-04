using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    // Static variables stay in memory across different scenes
    public static bool isPaused = false;
    public static bool wasPausedBeforeSettings = false;
    public static string previousSceneName; 

    void Start()
    {
        // Check if we just came back from the settings menu while the game was paused
        if (wasPausedBeforeSettings)
        {
            PauseGame();
            // Reset this so it doesn't stay paused forever if you leave and come back later
            wasPausedBeforeSettings = false; 
        }
        else
        {
            // Ensure the game is running if we aren't resuming a pause
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    void Update()
    {
        // P or Escape to pause/unpause
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;

        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(true);

        Time.timeScale = 0f;
        Debug.Log("Game Paused!");
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);

        Time.timeScale = 1f;
        Debug.Log("Game Resumed!");
    }

    public void GoToSettings()
    {
        // Store the pause state and the current scene name before leaving
        wasPausedBeforeSettings = isPaused;
        previousSceneName = SceneManager.GetActiveScene().name;
        
        // Reset timeScale so the Settings menu isn't frozen if you use animations there
        Time.timeScale = 1f;
        SceneManager.LoadScene("SettingsMenu");
    }

    public void ReturnFromSettings()
    {
        
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogWarning("Previous scene not set! Loading fallback to Level1.");
            SceneManager.LoadScene("Level1"); 
        }
    }

    public void QuitToMainMenu()
    {
        isPaused = false;
        wasPausedBeforeSettings = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
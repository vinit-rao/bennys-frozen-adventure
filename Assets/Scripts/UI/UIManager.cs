using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    public static bool isPaused = false;
    public static bool wasPausedBeforeSettings = false;
    public static string previousSceneName;

    void Start()
    {
        if (wasPausedBeforeSettings)
        {
            PauseGame();
            wasPausedBeforeSettings = false;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    void Update()
    {
        // p or escape to pause
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
        wasPausedBeforeSettings = isPaused;
        previousSceneName = SceneManager.GetActiveScene().name;
        Time.timeScale = 1f;
        SceneManager.LoadScene("SettingsMenu");
    }

    public void ReturnFromSettings()
    {
        // Go back to whatever scene we came from
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogWarning("Previous scene not set! Loading fallback.");
            SceneManager.LoadScene("Level1"); 
        }
    }
}
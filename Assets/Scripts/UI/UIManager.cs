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
            isPaused = false;
        }
    }

    void Update()
    {
      
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
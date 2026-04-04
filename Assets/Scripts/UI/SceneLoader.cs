using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameObject l2Button;
    private GameObject l3Button;

    private void Start()
    {
        l2Button = GameObject.Find("L2Btn");
        l3Button = GameObject.Find("L3Btn");

        // setActive false if level not passed
            l2Button.SetActive(PlayerPrefs.GetInt("L1Passed", 0) == 1);
            l3Button.SetActive(PlayerPrefs.GetInt("L2Passed", 0) == 1);
    }

    public void LoadTargetScene(string sceneName)
    {
        Time.timeScale = 1f;
        UIManager.isPaused = false;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
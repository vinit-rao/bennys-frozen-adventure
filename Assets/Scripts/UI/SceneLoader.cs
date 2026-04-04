using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameObject l2Btn;
    private GameObject l3Btn;
    private GameObject l2BtnLocked;
    private GameObject l3BtnLocked;


    private void Start()
    {
        l2Btn = GameObject.Find("L2Btn");
        l3Btn = GameObject.Find("L3Btn");
        l2BtnLocked = GameObject.Find("L2BtnLocked");
        l3BtnLocked = GameObject.Find("L3BtnLocked");

        bool l1Passed = PlayerPrefs.GetInt("L1Passed", 0) == 1;
        bool l2Passed = PlayerPrefs.GetInt("L2Passed", 0) == 1;

        SetUpBtn(l2Btn, l2BtnLocked, l1Passed);
        SetUpBtn(l3Btn, l3BtnLocked, l2Passed);
    }

    private void SetUpBtn(GameObject button, GameObject lockedButton, bool passed)
    {
        if (button != null)
        {
            button.SetActive(passed);
        }

        if (passed && lockedButton != null)
        {
            Destroy(lockedButton);
        }
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
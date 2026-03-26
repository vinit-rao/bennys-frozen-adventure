using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public string sceneToReturnTo;

    public void Back()
    {
        Time.timeScale = 1f;       
        UIManager.isPaused = false; 
        SceneManager.LoadScene(sceneToReturnTo);
    }
}
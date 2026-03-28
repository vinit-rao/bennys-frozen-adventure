using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DifficultyManager
{
public static string sceneName => SceneManager.GetActiveScene().name;
    
    public static int timeOrderBaseline
    {
        get
        {
            switch (sceneName)
            {
                case "Level1":
                    return 25;
                case "Level2":
                    return 15;
                case "Level3":
                    return 10;
                default:
                    return 25;
            }
        }
    }

    public static float spawnRate
    {
        get
        {
            switch (sceneName)
            {
                case "Level1":
                    return 2.0f;
                case "Level2":
                    return 1.5f;
                case "Level3":
                    return 1.0f;
                default:
                    return 2.0f;
            }
        }
    }
}

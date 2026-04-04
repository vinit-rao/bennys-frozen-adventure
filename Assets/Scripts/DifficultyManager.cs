using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DifficultyManager
{
public static string sceneName => SceneManager.GetActiveScene().name;
    
    public static float timeMultiplier
    {
        get
        {
            switch (sceneName)
            {
                case "Level1":
                    return 5f;
                case "Level2":
                    return 4.5f;
                case "Level3":
                    return 4f;
                default:
                    return 5f;
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
                    return 1.7f;
                case "Level2":
                    return 1.5f;
                case "Level3":
                    return 1.3f;
                default:
                    return 1.7f;
            }
        }
    }

    public static float fallSpeed
    {
        get
        {
            switch (sceneName)
            {
                case "Level1":
                    return 2.1f;
                case "Level2":
                    return 2.4f;
                case "Level3":
                    return 2.8f;
                default:
                    return 1.8f;
            }
        }
    }
}

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
                    return 3.8f;
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
                    return 2.0f;
                case "Level2":
                    return 1.7f;
                case "Level3":
                    return 1.3f;
                default:
                    return 2.0f;
            }
        }
    }

    public static float fallSpeed => sceneName switch {
    "Level1" => 1.8f,
    "Level2" => 2.3f,
    "Level3" => 2.8f,
    _        => 1.8f
};
}

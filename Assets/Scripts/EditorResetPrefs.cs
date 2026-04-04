using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reset PlayerPrefs every time play mode is started in the editor
public class EditorResetPrefs
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResetOnPlay()
    {
#if UNITY_EDITOR
        PlayerPrefs.DeleteAll();
#endif
    }
}

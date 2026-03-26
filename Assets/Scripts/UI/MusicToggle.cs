using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicToggle : MonoBehaviour
{
    public static MusicToggle instance;
    public Slider volumeSlider;
    private float currentVolume = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentVolume = PlayerPrefs.GetFloat("soundVolume", 1f);
        AudioListener.volume = currentVolume;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = currentVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float value)
    {
        currentVolume = value;
        AudioListener.volume = currentVolume;
        PlayerPrefs.SetFloat("soundVolume", currentVolume);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        volumeSlider = FindObjectOfType<Slider>();
        if (volumeSlider != null)
        {
            volumeSlider.value = currentVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }
}
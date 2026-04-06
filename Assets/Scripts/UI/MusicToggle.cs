using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicToggle : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
       
        if (musicSlider == null || sfxSlider == null)
        {
            Debug.LogError("Sliders not assigned");
            return;
        }

        StartCoroutine(InitializeSliders());

        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxChanged);

    }
    IEnumerator InitializeSliders()
    {
       yield return null;

       if (AudioManager.Instance != null)
        {
            musicSlider.value = AudioManager.Instance.masterMusicVolume;
            sfxSlider.value = AudioManager.Instance.masterSfxVolume;
        }
    }

    public void OnMusicChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }
    }

    public void OnSfxChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSfxVolume(value);
        }
    }
}
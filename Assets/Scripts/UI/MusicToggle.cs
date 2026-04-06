using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
       
        if (AudioManager.Instance != null)
        {
            musicSlider.value = AudioManager.Instance.masterMusicVolume;
            sfxSlider.value = AudioManager.Instance.masterSfxVolume;
        }
        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxChanged);
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
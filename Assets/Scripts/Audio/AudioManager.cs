using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneMusic
{
    public string sceneName;
    public AudioClip[] tracks;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Range(0f, 1f)] public float masterMusicVolume = 0.5f;
    [Range(0f, 1f)] public float masterSfxVolume = 1f;

    public SceneMusic[] sceneMusicMaps;

    public AudioClip[] clickSounds;
    public AudioClip[] moveSounds;
    public AudioClip[] catchSounds;

    public AudioClip levelEndSound;
    public AudioClip orderCompleteSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource.volume = masterMusicVolume;
            sfxSource.volume = masterSfxVolume;
            musicSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HookUpButtons();
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        foreach (SceneMusic map in sceneMusicMaps)
        {
            if (map.sceneName == sceneName && map.tracks.Length > 0)
            {
                AudioClip nextTrack = map.tracks[UnityEngine.Random.Range(0, map.tracks.Length)];

                if (musicSource.clip == nextTrack && musicSource.isPlaying) return;

                musicSource.clip = nextTrack;
                musicSource.Play();
                return;
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        masterMusicVolume = volume;
        musicSource.volume = masterMusicVolume;
    }

    public void SetSfxVolume(float volume)
    {
        masterSfxVolume = volume;
        sfxSource.volume = masterSfxVolume;
    }

    private void HookUpButtons()
    {
        Button[] allButtons = FindObjectsOfType<Button>(true);
        foreach (Button btn in allButtons)
        {
            btn.onClick.RemoveListener(PlayClickSound);
            btn.onClick.AddListener(PlayClickSound);
        }
    }

    public void PlayClickSound() => PlayRandomSound(clickSounds);
    public void PlayMoveSound() => PlayRandomSound(moveSounds);
    public void PlayCatchSound() => PlayRandomSound(catchSounds);
    public void PlayLevelEndSound() => PlaySingleSound(levelEndSound);
    public void PlayOrderCompleteSound() => PlaySingleSound(orderCompleteSound);

    private void PlayRandomSound(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        sfxSource.PlayOneShot(randomClip);
    }

    private void PlaySingleSound(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    private void OnValidate()
    {
        if (Application.isPlaying && musicSource != null && sfxSource != null)
        {
            musicSource.volume = masterMusicVolume;
            sfxSource.volume = masterSfxVolume;
        }
    }
}
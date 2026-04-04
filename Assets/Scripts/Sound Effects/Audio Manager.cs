using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Source")]
    [Tooltip("This must be dedicated ONLY to sound effects, not music!")]
    public AudioSource sfxSource;

    [Header("Audio Clips - Arrays")]
    public AudioClip[] clickSounds;
    public AudioClip[] moveSounds;
    public AudioClip[] catchSounds;

    [Header("Audio Clips - Singles")]
    public AudioClip levelEndSound;
    public AudioClip orderCompleteSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HookUpButtons;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HookUpButtons;
    }

    private void HookUpButtons(Scene scene, LoadSceneMode mode)
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

    //logic to random multi sounds
    private void PlayRandomSound(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;

        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        sfxSource.PlayOneShot(randomClip);
    }

    private void PlaySingleSound(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.PlayOneShot(clip);
    }
}
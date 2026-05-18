using UnityEngine;

public class MainSoundManager : MonoBehaviour
{
    public static MainSoundManager Instance;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;

            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;

            // Load and apply saved system settings volume
            SystemSettings settings = SystemSettings.Load();
            SetVolumes(settings.MasterVolume, settings.MusicVolume, settings.SFXVolume);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusicForDifficulty(string difficulty)
    {
        string trackPath = "Music/NormalMusic";

        switch (difficulty)
        {
            case "Easy":
                trackPath = "Music/EasyMusic";
                break;
            case "Hard":
                trackPath = "Music/HardMusic";
                break;
            case "Custom":
                trackPath = "Music/CustomMusic";
                break;
            case "Normal":
            default:
                trackPath = "Music/NormalMusic";
                break;
        }

        AudioClip clip = Resources.Load<AudioClip>(trackPath);
        if (clip != null)
        {
            if (musicSource.clip == clip && musicSource.isPlaying) return;

            musicSource.clip = clip;
            musicSource.Play();
            Debug.Log($"[SoundManager] Playing BGM: {trackPath}");
        }
        else
        {
            Debug.LogWarning($"[SoundManager] Music file not found at Resources/{trackPath}");
        }
    }

    public void PlaySFX(string sfxName)
    {
        string sfxPath = "SFX/" + sfxName;
        AudioClip clip = Resources.Load<AudioClip>(sfxPath);

        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"[SoundManager] SFX not found at Resources/{sfxPath}");
        }
    }

    public void SetVolumes(float master, float music, float sfx)
    {
        AudioListener.volume = master;
        if (musicSource != null) musicSource.volume = music;
        if (sfxSource != null) sfxSource.volume = sfx;
    }
}

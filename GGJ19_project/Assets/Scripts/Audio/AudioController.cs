using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffectType
{
    PICKUP = 0,
    SPOTTED,
    HOMEFANFARE
}

public enum MusicState
{
    NONE = 0,
    NORMAL,
    CATCHASE
}

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get { return GetInstance(); } }
    private static AudioController instance;
    private static AudioController GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<AudioController>();
        }
        return instance;
    }

    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip catNearbyMusic;
    [SerializeField] private AudioClip catChaseMusic;
    [SerializeField] private float maxMusicVolume = 0.5f;
    [SerializeField] private float backgroundMusicVolume = 0.5f;
    [SerializeField] private float minNearbyMusicDistanceCat = 5f;
    [SerializeField] private float maxNearbyMusicDistanceCat = 10f;

    [SerializeField] private bool playBackgroundMusic = false;

    [Header("Effects")]
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip spottedClip;
    [SerializeField] private AudioClip homeFanfareClip;

    [Header("Technical")]
    [SerializeField] private AudioSource backgroundMusicAudioSource;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    private bool fadeinMusic = true;

    private MusicState currentMusicState = MusicState.NORMAL;

    private void Start()
    {
        AudioController.Instance.SwitchMusicState(MusicState.NORMAL);
        
        // the idea is that the background music is playing unless catchase is
        if (playBackgroundMusic)
        {
            backgroundMusicAudioSource.clip = backgroundMusic;
            backgroundMusicAudioSource.Play();
            backgroundMusicAudioSource.volume = backgroundMusicVolume;
        }
    }

    private void Update()
    {
        switch (currentMusicState)
        {
            case MusicState.NORMAL:
                float distanceToCat = 9f; // TODO: Vector3.Distance(cat, mouse);
                float vol = Mathf.Clamp(maxMusicVolume - (1f - minNearbyMusicDistanceCat / distanceToCat), 0, maxMusicVolume);
                musicAudioSource.volume = vol;
                break;
            case MusicState.CATCHASE:
                if (fadeinMusic)
                {
                    musicAudioSource.volume = musicAudioSource.time;
                    if (musicAudioSource.volume >= maxMusicVolume)
                    {
                        musicAudioSource.volume = maxMusicVolume;
                        fadeinMusic = false;
                    }
                }
                break;
        }
    }

    public void SwitchMusicState(MusicState state)
    {
        currentMusicState = state;
        if (state == MusicState.CATCHASE)
        {
            backgroundMusicAudioSource.Stop();
        }
        else
        {
            backgroundMusicAudioSource.Play();
        }

        switch (state)
        {
            case MusicState.NONE:
                musicAudioSource.Stop();
                break;
            case MusicState.NORMAL:
                musicAudioSource.clip = catNearbyMusic;
                musicAudioSource.time = backgroundMusicAudioSource.time;
                musicAudioSource.Play();
                break;
            case MusicState.CATCHASE:
                PlayOneShot(SoundEffectType.SPOTTED);
                musicAudioSource.Stop();

                StartCoroutine(PauseMusicBeforeChase());
                break;
            default:
                Debug.LogError("Music state missing");
                break;
        }
    }

    public void PlayOneShot(SoundEffectType type)
    {
        switch (type)
        {
            case SoundEffectType.PICKUP:
                effectAudioSource.PlayOneShot(pickupClip);
                break;
            case SoundEffectType.SPOTTED:
                effectAudioSource.PlayOneShot(spottedClip);
                break;
            case SoundEffectType.HOMEFANFARE:
                effectAudioSource.PlayOneShot(homeFanfareClip);
                break;
            default:
                Debug.LogError("Sound effect state missing");
                break;
        }
    }

    private IEnumerator PauseMusicBeforeChase()
    {
        yield return new WaitForSeconds(1f);

        musicAudioSource.clip = catChaseMusic;
        musicAudioSource.Play();

        fadeinMusic = true;
        musicAudioSource.volume = 0f;
    }
}

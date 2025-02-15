using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource audioSource;
    public AudioClip defaultClip;
    public static event Action<AudioClip> OnPlaySound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        OnPlaySound += PlaySound;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else if (defaultClip != null)
        {
            audioSource.PlayOneShot(defaultClip);
        }
    }

    public static void Play(AudioClip clip = null)
    {
        OnPlaySound?.Invoke(clip);
    }

    public static void PlayDefault()
    {
        Play(null);
    }
}

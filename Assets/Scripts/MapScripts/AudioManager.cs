using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton instance

    public AudioSource bgmSource; // Background music source
    public AudioSource sfxSource;// Sound effects source

    public AudioClip[] musicClips;

    private void Awake()
    {
        // Create the singleton instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Play a sound effect
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Play background music
    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    // Stop background music
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}

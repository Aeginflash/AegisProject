using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton instance



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
    public void PlaySFX(AudioClip clip,float volume)
    {
        AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.volume = volume;
        sfxSource.PlayOneShot(clip);
    }

    // Play background music
    public void PlayBGM(AudioClip clip,float volume)
    {
        AudioSource bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = clip;
        bgmSource.volume = volume;
        bgmSource.Play();
        bgmSource.loop = true;
    }

    // Stop background music
    public void StopBGM()
    {
        AudioSource bgmSource=GetComponent<AudioSource>();
        bgmSource.Stop();
    }
}

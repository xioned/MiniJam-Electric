using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Singleton;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void PlaySFX(AudioClip audioClip)=> AudioManager.Singleton.PlaySfx_(audioClip);
    public void PlaySfx_(AudioClip audioClip)=> audioSource.PlayOneShot(audioClip);
}

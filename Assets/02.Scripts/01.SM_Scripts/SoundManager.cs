using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public float BGMVolume = 0.2f;
    public float EffectVolume = 0.6f;
    public float DoorVolume = 0.4f;

    public AudioClip[] soundEffects;
    public AudioClip bgmClip;

    [HideInInspector] public AudioSource startSource;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public AudioSource bgmAudioSource;
    [HideInInspector] public AudioSource outroAudioSource;
    [HideInInspector] public AudioSource trapAudioSource;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        startSource = gameObject.AddComponent<AudioSource>();
        audioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        outroAudioSource = gameObject.AddComponent<AudioSource>();
        trapAudioSource = gameObject.AddComponent<AudioSource>();

    }

    private void Start()
    {
        startSource.PlayOneShot(soundEffects[0], BGMVolume);
        Invoke("PlayBGM", soundEffects[0].length);
    }

    public void PlayBGM()
    {
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = BGMVolume;
        bgmAudioSource.Play();
    }

    public void PlayWalkEffect()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(soundEffects[1], EffectVolume);
        }
    }

    public void PlayDoorLockEffect()
    {
        audioSource.PlayOneShot(soundEffects[2], DoorVolume);
    }

    public void PlayDoorOpenEffect()
    {
        audioSource.PlayOneShot(soundEffects[3], DoorVolume);
    }


    public void PlayDoorErrorEffect()
    {
        audioSource.PlayOneShot(soundEffects[4], DoorVolume);
    }
    public void PlaytrapEffect()
    {
        trapAudioSource.PlayOneShot(soundEffects[5], EffectVolume);
    }

    public void PlayOutroEffect()
    {
        outroAudioSource.PlayOneShot(soundEffects[6], BGMVolume);
    }
}

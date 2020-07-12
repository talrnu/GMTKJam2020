using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private string _musicToPlay;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        // Play Music
        PlayMusic(_musicToPlay);
    }

    public void Playsound(string soundPath)
    {
        AudioClip clipToPlay = Resources.Load<AudioClip>(soundPath);
        _audioSource.PlayOneShot(clipToPlay);
    }

    public void PlayMusic(string songPath)
    {
        AudioClip musicToPlay = Resources.Load<AudioClip>(songPath);
        _backgroundMusic.clip = musicToPlay;
        _backgroundMusic.volume = .2f;

        _backgroundMusic.Play();
    }
}

using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource musicSource;
    public AudioSource playerSource;

    public void PlaySoundClip(AudioClip clip) {
        playerSource.clip = clip;
        playerSource.Play();
    }

    public void PlayMusic() {
        musicSource.Play();
    }
}
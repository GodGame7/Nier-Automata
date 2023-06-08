using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagFightSubTitleAudio : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioSource.volume = audioManager.float_talkSound;
    }

    public void PlayClip(int subTitleCounter)
    {
        if (subTitleCounter < audioClips.Length)
        {
            audioSource.clip = audioClips[subTitleCounter];
            audioSource.Play();
        }
    }

    public void SetBgmVolume(float volume = 1.0f)
    {
        audioSource.volume = volume;
    }
}


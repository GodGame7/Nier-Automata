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
    }

    public void PlayClip(int subTitleCounter)
    {
        if (subTitleCounter < audioClips.Length)
        {
            audioSource.clip = audioClips[subTitleCounter];
            audioSource.Play();
        }
    }
}


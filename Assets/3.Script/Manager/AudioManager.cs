using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Sound
{
    public string name;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [Header("BGM")]
    [SerializeField]
    private Sound[] bgmClips;
    [SerializeField]
    private float bgmVolume;
    private AudioSource bgmPlayer;

    [Header("SFX")]
    [Space(30)]
    [SerializeField]
    private float sfxVolume;
    [Tooltip("����� Ŭ�� �߰��ϰ� Define ��ũ��Ʈ�� enum �߰��ؾ���")]
    [SerializeField]
    private AudioClip[] sfxClips;
    // ī�޶� ��ġ(�߾�)���� ����� ȿ���� (ex-�÷��̾� ȿ����)
    private AudioSource sfxPlayer;

    public float float_talkSound;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (!Instance.Equals(this))
            {
                Destroy(gameObject);
            }
            return;
        }
        Init();
    }
    private void OnEnable()
    {
        // �̺�Ʈ ����
        // UI~~.OnBgmVolumeSetting += SetBgmVolume;
        // UI~~.OnSfxVolumeSetting += SetSfxVolume;
    }
    private void OnDisable()
    {
        // �̺�Ʈ ����
        // UI~~.OnBgmVolumeSetting -= SetBgmVolume;
        // UI~~.OnSfxVolumeSetting -= SetSfxVolume;
    }

    private void Init()
    {
        // BGM Player ���� �� ����
        GameObject bgm_obj = new GameObject("BgmPlayer");
        bgm_obj.transform.parent = transform;
        bgmPlayer = bgm_obj.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        SetBgmVolume();

        // SFX Player ���� �� ����
        GameObject sfx_obj = new GameObject("SfxPlayer");
        sfx_obj.transform.parent = transform;
        sfxPlayer = sfx_obj.AddComponent<AudioSource>();
        sfxPlayer.playOnAwake = false;
        SetSfxVolume();
    }

    public void PlayBgm(string name)
    {
        // �̹� ������� BGM�� ������ ����
        StopBgm();

        // �̸� ��ġ�ϴ� BGM ã�Ƽ� ���
        foreach (Sound s in bgmClips)
        {
            if (s.name.Equals(name))
            {
                bgmPlayer.clip = s.clip;
                bgmPlayer.Play();
                break;
            }
        }
    }
    public void StopBgm()
    {
        if (bgmPlayer.isPlaying)
        {
            bgmPlayer.Stop();
        }
    }

    public void SetBgmVolume(float volume = 1.0f)
    {
        bgmPlayer.volume = volume;
    }

    public void PlaySfx<T>(T sfx) where T : Enum
    {
        sfxPlayer.PlayOneShot(sfxClips[Convert.ToInt32(sfx)]);
    }
    public void SetSfxVolume(float volume = 1.0f)
    {
        sfxPlayer.volume = volume;
    }
}

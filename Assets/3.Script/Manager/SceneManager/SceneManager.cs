using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class SceneManager : MonoBehaviour
{
    [Header("MainCamera")]
    [SerializeField] Camera camera;

    [Header("MainCamera")]
    [SerializeField] PlayerInput player1;
    [SerializeField] StateManager player2;

    [Header("Enemy")]
    [SerializeField] GameObject[] em0000;
    [SerializeField] GameObject[] em0000_2;
    [SerializeField] GameObject em0010;
    [SerializeField] GameObject em1000;

    [SerializeField] float respawnTime = 2f;
    //public UnityEvent enemyspawner;

    [Header("벽뿌")]
    [SerializeField] Animator Wallanim;
    [SerializeField] GameObject Wall;


    [Header("순서 확인용")]
    public bool first = true;
    public bool second = false;
    public bool third = false;

    [HideInInspector]
    public VideoPlayer video;

    [Header("비디오 넣자")]
    public GameObject Raw;
    public VideoClip firstvideo;
    public VideoClip secondvideo;
    public VideoClip thirdvideo;


    private void Awake()
    {
        TryGetComponent(out video);
    }

    private void Start()
    {
        StartCoroutine(firstVideo());
    }

    private void Update()
    {

        if (first)
        {
            StartCoroutine(firstEnemy());
        }
        else if (second)
        {
            StartCoroutine(SecondEnemy());
        }
        else if (third)
        {
            if (!em0010.activeSelf)
            {
                StartCoroutine(Boss());
            }
        }
    }



    IEnumerator firstVideo()
    {
        camera.GetComponent<CameraMovement>().enabled = false;
        player1.enabled = false;
        player2.enabled = false;

        Time.timeScale = 0f;
        
        Raw.SetActive(true);
        video.clip = firstvideo;
        video.Play();

        while(!video.isPlaying)
        {
            yield return null;
        }
        yield return new WaitUntil(() => !video.isPlaying);
        Time.timeScale = 1f;
        Raw.SetActive(false);

        camera.GetComponent<CameraMovement>().enabled = true;
        player1.enabled = true;
        player2.enabled = true;
    }


    IEnumerator firstEnemy()
    {
        first = false;

        while (true)
        {
            bool allInactive = true;

            for (int i = 0; i < em0000.Length; i++)
            {
                if (em0000[i].activeSelf)
                {
                    allInactive = false;
                    break;
                }
            }

            if (allInactive)
            {
                yield return new WaitForSeconds(respawnTime);

                for (int i = 0; i < em0000_2.Length; i++)
                {
                    em0000_2[i].SetActive(true);
                }
                second = true;
                break;
            }
            yield return null;
        }

    }

    IEnumerator SecondEnemy()
    {
        second = false;

        while (true)
        {
            bool allInactive = true;

            for (int i = 0; i < em0000_2.Length; i++)
            {
                if (em0000_2[i].activeSelf)
                {
                    allInactive = false;
                    break;
                }
            }

            if (allInactive)
            {
                yield return new WaitForSeconds(respawnTime);

                em0010.SetActive(true);
                third = true;
                break;
            }
            yield return null;
        }
    }

    IEnumerator Boss()
    {
        third = false;
        video.clip = thirdvideo;
        video.Play();
        Time.timeScale = 0f;

        while (!video.isPlaying)
        {
            yield return null;
        }
        yield return new WaitUntil(() => !video.isPlaying);

        Time.timeScale = 1f;
        Wall.SetActive(false);

        if (!em1000.activeSelf)
        {
            em1000.SetActive(true);
        }

        yield return new WaitForSeconds(4.5f);
        Wallanim.SetTrigger("Breaken");
    }

}

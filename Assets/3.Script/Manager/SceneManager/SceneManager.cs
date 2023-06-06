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
    //[SerializeField] MAin player2;

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
    public bool fourth = false;

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
        StartCoroutine(firstVideo_co());
    }

    private void Update()
    {

        if (first)
        {
            StartCoroutine(firstEnemy_co());
        }
        else if (second)
        {
            StartCoroutine(SecondEnemy_co());
        }
        else if (third)
        {
            if (!em0010.activeSelf)
            {
                StartCoroutine(Boss_co());
            }
        }
        else if (fourth)
        {
            StartCoroutine(BossEnd_co());
        }
    }



    IEnumerator firstVideo_co()
    {
        //화면전환
        Time.timeScale = 0f;
        camera.GetComponent<CameraMovement>().enabled = false;
        player1.enabled = false;
        //비디오 출력
        video.clip = firstvideo;
        video.Play();

        Raw.SetActive(true);


        while (!video.isPlaying)
        {
            yield return null;
        }
        yield return new WaitUntil(() => !video.isPlaying);

        //화면전환
        Time.timeScale = 1f;
        camera.GetComponent<CameraMovement>().enabled = true;
        player1.enabled = true;
        Raw.SetActive(false);
    }


    IEnumerator firstEnemy_co()
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

    IEnumerator SecondEnemy_co()
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

    IEnumerator Boss_co()
    {
        third = false;
        Raw.SetActive(true);
        camera.GetComponent<CameraMovement>().enabled = false;
        player1.enabled = false;
        video.clip = secondvideo;
        video.Play();
        Time.timeScale = 0f;

        while (!video.isPlaying)
        {
            yield return null;
        }
        yield return new WaitUntil(() => !video.isPlaying);

        camera.GetComponent<CameraMovement>().enabled = true;
        Raw.SetActive(false);
        player1.enabled = true;
        Time.timeScale = 1f;
        Wall.SetActive(false);

        if (!em1000.activeSelf)
        {
            em1000.SetActive(true);
        }

        yield return new WaitForSeconds(4.5f);
        Wallanim.SetTrigger("Breaken");

        fourth = true;
    }

    IEnumerator BossEnd_co()
    {

        if (em1000.GetComponent<EnemyHp>().currentHp <= 0)
        {
            fourth = false;
            yield return new WaitForSeconds(0.6f);

            video.clip = thirdvideo;
            video.Play();

            camera.GetComponent<CameraMovement>().enabled = false;
            player1.enabled = false;
            Raw.SetActive(true);

            while (!video.isPlaying)
            {
                yield return null;
            }
            yield return new WaitUntil(() => !video.isPlaying);

            camera.GetComponent<CameraMovement>().enabled = true;
            player1.enabled = true;
            Raw.SetActive(false);

        }
    }

}

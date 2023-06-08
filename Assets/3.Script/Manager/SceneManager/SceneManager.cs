using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class SceneManager : MonoBehaviour
{
    [Header("MainCamera")]
    [SerializeField] new Camera camera;

    [Header("꺼야될거")]
    [SerializeField] PlayerInput player1;
    [SerializeField] StateManager player2;
    [SerializeField] MenuUI menuUI;

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

    [Header("오디오, 자막 관련")]
    public AudioManager audioManager;
    public MainSubTitleTextManager subText;


    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        TryGetComponent(out video);
    }

    private void Start()
    {
        // BGM 시작
        audioManager.SetBgmVolume(0.5f);

        StartCoroutine(firstVideo_co());
    }

    private void Update()
    {
        if (video.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                video.Stop();
            }
        }


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

        //video.clip = firstvideo;
        video.Play();

        //비디오 출력
        VideoStart();

        //끝날떄까지
        while (!video.isPlaying)
        {
            yield return null;
        }
        yield return new WaitUntil(() => !video.isPlaying);

        //비디오 닫기
        VideoEnd();

        StartBGM();

        //미리 비디오 바꿔두기
        video.clip = secondvideo;
        video.Stop();
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


        // BGM 멈춤
        StopBGM(); 

        //비디오 시작
        VideoStart();
        //비디오 플레이
        video.Play();

        while (!video.isPlaying)
        {
            yield return null;
        }
        yield return new WaitUntil(() => !video.isPlaying);

        VideoEnd();

        Wall.SetActive(false);

        video.clip = thirdvideo;
        video.Stop();

        StartBGM(); // BGM 시작

        video.clip = thirdvideo;

        if (!em1000.activeSelf)
        {
            em1000.SetActive(true);
        }

        yield return new WaitForSeconds(4.5f);
        Wallanim.SetTrigger("Breaken");
        fourth = true;

        yield return new WaitForSeconds(1f);
        subText.PlayClip(0);
        subText.NextSubText(); // 이것이....... 목표 대형 병기!? <- 자막출력

        yield return new WaitForSeconds(4f);
        subText.NextSubText(); // 부정: 해당 적은 목표가 아님 <- 자막출력

        yield return new WaitForSeconds(3.5f);
        subText.NextSubText();  // 신속한 제거를 권장 <- 자막출력
        yield return new WaitForSeconds(2.5f);
        subText.NextSubText(); // 간단하게 말하는군 <- 자막출력

        yield return new WaitForSeconds(2f);
        subText.OffSubText();
    }

    IEnumerator BossEnd_co()
    {

        if (em1000.GetComponent<EnemyHp>().currentHp <= 0)
        {
            fourth = false;
            yield return new WaitForSeconds(0.6f);

            StartBGM(); // BGM 멈춤


            video.Play();
            VideoStart();
            while (!video.isPlaying)
            {
                yield return null;
            }
            yield return new WaitUntil(() => !video.isPlaying);
            VideoEnd();


            StopBGM(); // BGM 시작
        }
    }

    // BGM 재생 메소드
    void StartBGM()
    {
        audioManager.PlayBgm("BGM");
    }

    // BGM 멈춤 메소드
    void StopBGM()
    {
        audioManager.StopBgm();
    }

    void VideoStart()
    {
        //화면전환
        Time.timeScale = 0f;
        camera.GetComponent<CameraMovement>().enabled = false;
        player1.enabled = false;
        player2.enabled = false;
        menuUI.enabled = false;

        Raw.SetActive(true);
    }

    void VideoEnd()
    {
        //화면전환
        Time.timeScale = 1f;
        camera.GetComponent<CameraMovement>().enabled = true;
        player1.enabled = true;
        player2.enabled = true;
        menuUI.enabled = true;


        Raw.SetActive(false);
    }

}

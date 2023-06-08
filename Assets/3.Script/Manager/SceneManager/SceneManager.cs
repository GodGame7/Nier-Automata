using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class SceneManager : MonoBehaviour
{
    [Header("MainCamera")]
    [SerializeField] new Camera camera;

    [Header("���ߵɰ�")]
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

    [Header("����")]
    [SerializeField] Animator Wallanim;
    [SerializeField] GameObject Wall;


    [Header("���� Ȯ�ο�")]
    public bool first = true;
    public bool second = false;
    public bool third = false;
    public bool fourth = false;

    [HideInInspector]
    public VideoPlayer video;

    [Header("���� ����")]
    public GameObject Raw;
    public VideoClip firstvideo;
    public VideoClip secondvideo;
    public VideoClip thirdvideo;

    [Header("�����, �ڸ� ����")]
    public AudioManager audioManager;
    public MainSubTitleTextManager subText;


    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        TryGetComponent(out video);
    }

    private void Start()
    {
        // BGM ����
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

        //���� ���
        VideoStart();

        //����������
        while (!video.isPlaying)
        {
            yield return null;
        }
        yield return new WaitUntil(() => !video.isPlaying);

        //���� �ݱ�
        VideoEnd();

        StartBGM();

        //�̸� ���� �ٲ�α�
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


        // BGM ����
        StopBGM(); 

        //���� ����
        VideoStart();
        //���� �÷���
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

        StartBGM(); // BGM ����

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
        subText.NextSubText(); // �̰���....... ��ǥ ���� ����!? <- �ڸ����

        yield return new WaitForSeconds(4f);
        subText.NextSubText(); // ����: �ش� ���� ��ǥ�� �ƴ� <- �ڸ����

        yield return new WaitForSeconds(3.5f);
        subText.NextSubText();  // �ż��� ���Ÿ� ���� <- �ڸ����
        yield return new WaitForSeconds(2.5f);
        subText.NextSubText(); // �����ϰ� ���ϴ±� <- �ڸ����

        yield return new WaitForSeconds(2f);
        subText.OffSubText();
    }

    IEnumerator BossEnd_co()
    {

        if (em1000.GetComponent<EnemyHp>().currentHp <= 0)
        {
            fourth = false;
            yield return new WaitForSeconds(0.6f);

            StartBGM(); // BGM ����


            video.Play();
            VideoStart();
            while (!video.isPlaying)
            {
                yield return null;
            }
            yield return new WaitUntil(() => !video.isPlaying);
            VideoEnd();


            StopBGM(); // BGM ����
        }
    }

    // BGM ��� �޼ҵ�
    void StartBGM()
    {
        audioManager.PlayBgm("BGM");
    }

    // BGM ���� �޼ҵ�
    void StopBGM()
    {
        audioManager.StopBgm();
    }

    void VideoStart()
    {
        //ȭ����ȯ
        Time.timeScale = 0f;
        camera.GetComponent<CameraMovement>().enabled = false;
        player1.enabled = false;
        player2.enabled = false;
        menuUI.enabled = false;

        Raw.SetActive(true);
    }

    void VideoEnd()
    {
        //ȭ����ȯ
        Time.timeScale = 1f;
        camera.GetComponent<CameraMovement>().enabled = true;
        player1.enabled = true;
        player2.enabled = true;
        menuUI.enabled = true;


        Raw.SetActive(false);
    }

}

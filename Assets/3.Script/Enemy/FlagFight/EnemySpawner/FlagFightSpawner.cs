using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Events;

public class FlagFightSpawner : MonoBehaviour
{
    [Header("Enemy를 넣어주세요.")]
    public GameObject[] em0030s;
    public GameObject[] em0031s;
    public GameObject[] em0032s;

    [Header("Managers")]
    [SerializeField] FlagFightSubTitleManager flagFightSubTitleManager;

    public FlagEmInformation FlagPrefab;
    private Vector3 emPosition;
    private Vector3 emRotation;
    private Vector3 emFirstDesPosition;
    private Vector3 emLastDesPosition;
    private Vector3 emRotatePoint;
    private Vector3 emRotateAxis;
    private float emfirstMoveSpeed;
    private float emlastMoveSpeed;
    private bool isCanLook;

    // 적이 없으면 이벤트를 발생시킴
    public int RemainEnemies;

    #region 발생시킬 이벤트들
    public UnityEvent phase1_15_EMDie; // phase2 시작
    public UnityEvent phase2_01_EMDie; // phase3 시작
    public UnityEvent phase3_04_EMDie; // phase4 시작 
    public UnityEvent phase4_01_EMDie; // Phase5 시작
    public UnityEvent Phase5_02_EMDie; // Phase6 시작
    public UnityEvent Phase6_01_EMDie; // Phase7 시작
    public UnityEvent Phase7_06_EMDie; // Phase8 시작
    public UnityEvent Phase8_01_EMDie; // Phase9 시작
    public UnityEvent Phase9_01_EMDie; // Phase10 시작
    public UnityEvent Phase10_01_EMDie; // Phase11 시작

    #endregion

    #region WaitForSeconds 모음
    private WaitForSeconds wait_half_Second = new WaitForSeconds(0.5f);
    private WaitForSeconds wait_1_Second = new WaitForSeconds(1.0f);

    #endregion

    private void Awake()
    {
        #region 이벤트 add
        flagFightSubTitleManager.phase1_15.AddListener(Phase1_15);
        flagFightSubTitleManager.phase2_01.AddListener(Phase2_01);
        flagFightSubTitleManager.phase3_04.AddListener(Phase3_04);
        flagFightSubTitleManager.phase4_01.AddListener(Phase4_01);
        flagFightSubTitleManager.phase5_02.AddListener(Phase5_02);
        flagFightSubTitleManager.phase6_01.AddListener(Phase6_01);
        flagFightSubTitleManager.phase7_06.AddListener(Phase7_06);

        Phase7_06_EMDie.AddListener(Phase8_01);
        Phase8_01_EMDie.AddListener(Phase9_01);
        Phase9_01_EMDie.AddListener(Phase10_01);
        Phase10_01_EMDie.AddListener(Phase11_01);



        #endregion
    }

    // Em0030 생성 메소드
    private void SpawnEm0030(Vector3 emRotation, Vector3 emPosition, Vector3 emFirstDesPosition, Vector3 emLastDesPosition, float emfirstMoveSpeed, float emlastMoveSpeed, bool isCanLook, int num)
    {
        em0030s[num].SetActive(true);
        em0030s[num].transform.rotation = Quaternion.Euler(emRotation);
        em0030s[num].transform.position = emPosition;
        Em0030Movement em0030Movement = em0030s[num].GetComponent<Em0030Movement>();
        em0030Movement.firstMoveSpeed = emfirstMoveSpeed;
        em0030Movement.lastMoveSpeed = emlastMoveSpeed;
        em0030Movement.desPos = emFirstDesPosition;
        em0030Movement.lastDesPos = emLastDesPosition;
        em0030Movement.isCanLook = isCanLook;
        em0030Movement.isReady = false;
        StartCoroutine(em0030Movement.Move_co());
    }

    // Em0031 생성 메소드
    private void SpawnEm0031(Vector3 emRotation, Vector3 emPosition, Vector3 emFirstDesPosition, int num)
    {
        em0031s[num].SetActive(true);
        em0031s[num].transform.rotation = Quaternion.Euler(emRotation);
        em0031s[num].transform.position = emPosition;
        Em0031Movement em0031Movement = em0031s[num].GetComponent<Em0031Movement>();
        em0031Movement.desPos = emFirstDesPosition;
        StartCoroutine(em0031Movement.Move_co());
    }

    // Em0032 생성 메소드
    private void SpawnEm0032(Vector3 emRotation, Vector3 emPosition, Vector3 emFirstDesPosition, Vector3 emRotatePoint, Vector3 emRotateAxis, float emfirstMoveSpeed, float emlastMoveSpeed, bool isCanLook, int num)
    {
        em0032s[num].SetActive(true);
        em0032s[num].transform.rotation = Quaternion.Euler(emRotation);
        em0032s[num].transform.position = emPosition;
        Em0032Movement em0032Movement = em0032s[num].GetComponent<Em0032Movement>();
        em0032Movement.desPos = emFirstDesPosition;
        em0032Movement.RotatePoint = emRotatePoint;
        em0032Movement.RotateAxis = emRotateAxis;
        em0032Movement.firstMoveSpeed = emfirstMoveSpeed;
        em0032Movement.lastMoveSpeed = emlastMoveSpeed;
        em0032Movement.isCanLook = isCanLook;
        em0032Movement.isReady = false;
        StartCoroutine(em0032Movement.Move_co());

    }


    // em0030 4기를 생성 // 0, 1, 2, 3
    public void Phase1_15()
    {
        RemainEnemies = 4;
        emRotation = Vector3.zero;
        emfirstMoveSpeed = 0.05f;
        emlastMoveSpeed = 0.001f;
        isCanLook = true;

        emPosition = new Vector3(-0.15f, 0.00f, 0.30f);
        emFirstDesPosition = new Vector3(-0.15f, 0.00f, 0.10f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 0);

        emPosition = new Vector3(-0.05f, 0.00f, 0.30f);
        emFirstDesPosition = new Vector3(-0.05f, 0.00f, 0.10f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 1);

        emPosition = new Vector3(0.05f, 0.00f, 0.30f);
        emFirstDesPosition = new Vector3(0.05f, 0.00f, 0.10f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 2);

        emPosition = new Vector3(0.15f, 0.00f, 0.30f);
        emFirstDesPosition = new Vector3(0.15f, 0.00f, 0.10f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 3);

        StartCoroutine(Co_CheckNumEm01_15());

    }

    IEnumerator Co_CheckNumEm01_15()
    {
        while (true)
        {
            if (RemainEnemies <= 0)
            {
                phase1_15_EMDie.Invoke();
                break;
            }
            yield return null;
        }
    }

    // em0032 10기를 생성 // 0, 1, 2, 3, 4, 5, 6, 7, 8, 9
    public void Phase2_01()
    {
        RemainEnemies = 10;
        emRotation = Vector3.zero;
        emRotateAxis = Vector3.up;
        emfirstMoveSpeed = 0.05f;
        emlastMoveSpeed = 8.00f;
        isCanLook = true;

        StartCoroutine(Co_Phase2_01());
    }

    IEnumerator Co_Phase2_01()
    {
        for (int i = 0; i < 10; i += 2)
        {
            emPosition = new Vector3(-0.25f, 0.00f, 0.30f);
            emFirstDesPosition = new Vector3(-0.20f, 0.00f, 0.20f);
            emRotatePoint = new Vector3(0.30f, 0.00f, 0.725f);
            emRotateAxis = Vector3.down;
            SpawnEm0032(emRotation, emPosition, emFirstDesPosition, emRotatePoint, emRotateAxis, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, i);
            yield return wait_half_Second;

            emPosition = new Vector3(0.25f, 0.00f, 0.30f);
            emFirstDesPosition = new Vector3(0.20f, 0.00f, 0.20f);
            emRotatePoint = new Vector3(-0.30f, 0.00f, 0.725f);
            emRotateAxis = Vector3.up;
            SpawnEm0032(emRotation, emPosition, emFirstDesPosition, emRotatePoint, emRotateAxis, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, i + 1);
            yield return wait_half_Second;
        }
        StartCoroutine(Co_CheckNumEm02_01());
    }

    IEnumerator Co_CheckNumEm02_01()
    {
        while (true)
        {
            if (RemainEnemies <= 0)
            {
                phase2_01_EMDie.Invoke();
                break;
            }
            yield return null;
        }
    }

    // em0030 6기를 생성 // 4, 5, 6, 7, 8, 9
    public void Phase3_04()
    {
        RemainEnemies = 6;
        emRotation = Vector3.zero;
        emfirstMoveSpeed = 0.05f;
        isCanLook = true;

        StartCoroutine(Co_Phase3_04());
    }

    IEnumerator Co_Phase3_04()
    {
        for (int i = 4; i < 10; i += 2)
        {
            emPosition = new Vector3(-0.30f, 0.00f, 0.09f);
            
            emFirstDesPosition = new Vector3(-0.28f, 0.00f, 0.09f);
            emLastDesPosition = new Vector3(0.50f, 0.00f, 0.15f);
            SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emfirstMoveSpeed, isCanLook, i);
            yield return wait_half_Second;

            emPosition = new Vector3(0.30f, 0.00f, 0.11f);
            emFirstDesPosition = new Vector3(0.28f, 0.00f, 0.11f);
            emLastDesPosition = new Vector3(-0.50f, 0.00f, 0.05f);
            SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emfirstMoveSpeed, isCanLook, i+1);
            yield return wait_half_Second;
        }

        StartCoroutine(Co_CheckNumEm03_04());

    }

    IEnumerator Co_CheckNumEm03_04()
    {
        while (true)
        {
            if (RemainEnemies <= 0)
            {
                phase3_04_EMDie.Invoke();
                break;
            }
            yield return null;
        }
    }

    // em0030 4기를 생성 // 0, 1, 2, 3
    public void Phase4_01()
    {
        RemainEnemies = 4;
        emRotation = Vector3.zero;
        emfirstMoveSpeed = 0.3f;
        emlastMoveSpeed = 0.001f;
        isCanLook = true;

        emPosition = new Vector3(-0.10f, 0.00f, -0.30f);
        emFirstDesPosition = new Vector3(-0.20f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 0);

        emPosition = new Vector3(-0.10f, 0.00f, -0.30f);
        emFirstDesPosition = new Vector3(-0.10f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 1);

        emPosition = new Vector3(0.10f, 0.00f, -0.30f);
        emFirstDesPosition = new Vector3(0.10f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 2);

        emPosition = new Vector3(0.10f, 0.00f, -0.30f);
        emFirstDesPosition = new Vector3(0.20f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 3);

        StartCoroutine(Co_CheckNumEm04_01());
    }

    IEnumerator Co_CheckNumEm04_01()
    {
        while (true)
        {
            if (RemainEnemies <= 0)
            {
                phase4_01_EMDie.Invoke();
                break;
            }
            yield return null;
        }
    }

    // em0030 8기를 생성 // 10, 11, 12, 13, 14, 15, 16, 17
    public void Phase5_02()
    {
        RemainEnemies = 8;
        emRotation = Vector3.zero;
        emfirstMoveSpeed = 0.00450f;
        emlastMoveSpeed = 0.0f;
        isCanLook = true;

        emPosition = new Vector3(0.15f, 0.00f, 0.30f);
        emFirstDesPosition = new Vector3(0.075f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 10);

        emPosition = new Vector3(0.30f, 0.00f, 0.15f);
        emFirstDesPosition = new Vector3(0.15f, 0.00f, 0.075f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 11);

        emPosition = new Vector3(0.30f, 0.00f, -0.15f);
        emFirstDesPosition = new Vector3(0.15f, 0.00f, -0.075f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 12);

        emPosition = new Vector3(0.15f, 0.00f, -0.30f);
        emFirstDesPosition = new Vector3(0.075f, 0.00f, -0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 13);

        emPosition = new Vector3(-0.15f, 0.00f, -0.30f);
        emFirstDesPosition = new Vector3(-0.075f, 0.00f, -0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 14);

        emPosition = new Vector3(-0.30f, 0.00f, -0.15f);
        emFirstDesPosition = new Vector3(-0.15f, 0.00f, -0.075f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 15);

        emPosition = new Vector3(-0.30f, 0.00f, 0.15f);
        emFirstDesPosition = new Vector3(-0.15f, 0.00f, 0.075f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 16);

        emPosition = new Vector3(-0.15f, 0.00f, 0.30f);
        emFirstDesPosition = new Vector3(-0.075f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 17);

        StartCoroutine(Co_CheckNumEm05_02());
    }

    IEnumerator Co_CheckNumEm05_02()
    {
        while (true)
        {
            if (RemainEnemies <= 0)
            {
                Phase5_02_EMDie.Invoke();
                break;
            }
            yield return null;
        }
    }

    // em0032 18기와 em0031 3기 생성 // 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27 / 0, 1, 2
    public void Phase6_01()
    {
        RemainEnemies = 21;
        emRotation = Vector3.zero;
        emRotatePoint = Vector3.zero;
        emfirstMoveSpeed = 0.05f;
        emlastMoveSpeed = 12.50f;
        isCanLook = true;

        StartCoroutine(Co_Phase6_01());
    }

    IEnumerator Co_Phase6_01()
    {
        emPosition = new Vector3(-0.23f, 0.00f, -0.27f);
        emFirstDesPosition = new Vector3(0.07f, 0.00f, -0.07f);
        emRotateAxis = Vector3.down;

        for (int i = 10; i < 13; i++)
        {
            SpawnEm0032(emRotation, emPosition, emFirstDesPosition, emRotatePoint, emRotateAxis, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, i);
            yield return wait_1_Second;
        }

        emPosition = new Vector3(0.23f, 0.00f, 0.27f);
        emFirstDesPosition = new Vector3(-0.07f, 0.00f, 0.07f);
        emRotateAxis = Vector3.down;

        for(int i = 13; i < 16; i++)
        {
            SpawnEm0032(emRotation, emPosition, emFirstDesPosition, emRotatePoint, emRotateAxis, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, i);
            yield return wait_1_Second;
        }

        for (int i = 16; i < 27; i += 2)
        {
            emPosition = new Vector3(-0.09f, 0.00f, 0.31f);
            emFirstDesPosition = new Vector3(0.11f, 0.00f, 0.11f);
            emRotateAxis = Vector3.up;
            SpawnEm0032(emRotation, emPosition, emFirstDesPosition, emRotatePoint, emRotateAxis, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, i);
            yield return wait_1_Second;

            emPosition = new Vector3(0.11f, 0.00f, 0.29f);
            emFirstDesPosition = new Vector3(-0.09f, 0.00f, 0.09f);
            emRotateAxis = Vector3.down;
            SpawnEm0032(emRotation, emPosition, emFirstDesPosition, emRotatePoint, emRotateAxis, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, i + 1);
            yield return wait_1_Second;
        }

        emPosition = new Vector3(-0.30f, 0.00f, 0.15f);
        emFirstDesPosition = new Vector3(0.40f, 0.00f, 0.15f);

        for (int i = 0; i < 3; i++)
        {
            SpawnEm0031(emRotation, emPosition, emFirstDesPosition, i);
            yield return wait_1_Second;
        }

        StartCoroutine(Co_CheckNumEm06_01());
    }

    IEnumerator Co_CheckNumEm06_01()
    {
        while (true)
        {
            if (RemainEnemies <= 0)
            {
                Phase6_01_EMDie.Invoke();
                break;
            }
            yield return null;
        }
    }

    // Side View
    // em0030 4기를 생성 // 0, 1, 2, 3
    public void Phase7_06()
    {
        RemainEnemies = 4;
        emRotation = Vector3.zero;
        emfirstMoveSpeed = 0.05f;
        emlastMoveSpeed = 0.001f;
        isCanLook = false;

        emPosition = new Vector3(0.00f, 0.20f, 0.25f);
        emFirstDesPosition = new Vector3(0.00f, 0.18f, 0.23f);
        emLastDesPosition = new Vector3(0.00f, 0.15f, 0.20f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 0);

        emPosition = new Vector3(0.00f, 0.15f, 0.25f);
        emFirstDesPosition = new Vector3(0.00f, 0.13f, 0.23f);
        emLastDesPosition = new Vector3(0.00f, 0.10f, 0.20f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 0);

        emPosition = new Vector3(0.00f, -0.15f, 0.25f);
        emFirstDesPosition = new Vector3(0.00f, -0.13f, 0.23f);
        emLastDesPosition = new Vector3(0.00f, -0.10f, 0.20f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 0);

        emPosition = new Vector3(0.00f, -0.20f, 0.25f);
        emFirstDesPosition = new Vector3(0.00f, -0.18f, 0.23f);
        emLastDesPosition = new Vector3(0.00f, -0.15f, 0.20f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, 0);

        StartCoroutine(Co_CheckNumEm07_06());
    }

    IEnumerator Co_CheckNumEm07_06()
    {
        while (true)
        {
            if (RemainEnemies <= 0)
            {
                Phase7_06_EMDie.Invoke();
                break;
            }
            yield return null;
        }
    }

    // em0032 6기를 생성 // 10, 11, 12, 13, 14, 15
    public void Phase8_01()
    {
        RemainEnemies = 6;
        emRotation = Vector3.zero;
        emfirstMoveSpeed = 0.05f;
        emlastMoveSpeed = 8.00f;
        isCanLook = false;

        StartCoroutine(Co_Phase8_01());
    }

    IEnumerator Co_Phase8_01()
    {
        for (int i = 10; i < 16; i += 2)
        {
            emPosition = new Vector3(0.00f, 0.15f, -0.30f);
            emFirstDesPosition = new Vector3(0.00f, 0.15f, -0.15f);
            emRotatePoint = new Vector3(0.00f, -0.15f, -0.15f);
            emRotateAxis = Vector3.right;
            SpawnEm0032(emRotation, emPosition, emFirstDesPosition, emRotatePoint, emRotateAxis, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, i);
            yield return wait_1_Second;

            emPosition = new Vector3(0.00f, -0.15f, -0.30f);
            emFirstDesPosition = new Vector3(0.00f, -0.15f, -0.15f);
            emRotatePoint = new Vector3(0.00f, 0.15f, -0.15f);
            emRotateAxis = Vector3.left;
            SpawnEm0032(emRotation, emPosition, emFirstDesPosition, emRotatePoint, emRotateAxis, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, i);
            yield return wait_1_Second;
        }

        StartCoroutine(Co_CheckNumEm08_01());
    }

    IEnumerator Co_CheckNumEm08_01()
    {
        while (true)
        {
            if (RemainEnemies <= 0)
            {
                Phase8_01_EMDie.Invoke();
                break;
            }
            yield return null;
        }
    }

    // em 0032 3기를 생성 // 16, 17, 18
    public void Phase9_01()
    {
        RemainEnemies = 3;
        emRotation = Vector3.zero;
        emfirstMoveSpeed = 0.05f;
        emlastMoveSpeed = 8.00f;
        isCanLook = false;

        StartCoroutine(Co_Phase9_01());
    }

    IEnumerator Co_Phase9_01()
    {
        for (int i = 16; i < 19; i++)
        {
            emPosition = new Vector3(0.00f, 0.10f, -0.30f);
            emFirstDesPosition = new Vector3(0.00f, 0.10f, 0.10f);
            emRotatePoint = new Vector3(0.00f, -0.00f, -0.00f);
            emRotateAxis = Vector3.right;
            SpawnEm0032(emRotation, emPosition, emFirstDesPosition, emRotatePoint, emRotateAxis, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, i);
            yield return wait_1_Second;
        }

        StartCoroutine(Co_CheckNumEm09_01());
    }

    IEnumerator Co_CheckNumEm09_01()
    {
        while (true)
        {
            if (RemainEnemies <= 0)
            {
                Phase9_01_EMDie.Invoke();
                break;
            }
            yield return null;
        }
    }

    // em 0030 6기를 생성 // 10, 11, 12, 13, 14, 15
    public void Phase10_01()
    {
        RemainEnemies = 6;
        emRotation = Vector3.zero;
        emfirstMoveSpeed = 0.05f;
        isCanLook = true;

        StartCoroutine(Co_Phase10_01());
    }

    IEnumerator Co_Phase10_01()
    {
        emPosition = new Vector3(0.00f, 0.30f, 0.20f);
        emFirstDesPosition = new Vector3(0.00f, 0.24f, 0.19f);
        emLastDesPosition = new Vector3(0.00f, -0.60f, 0.05f);
        for (int i = 10; i < 13; i++)
        {
            SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, i);
            yield return wait_1_Second;
        }

        emPosition = new Vector3(0.00f, -0.30f, 0.07f);
        emFirstDesPosition = new Vector3(0.00f, -0.24f, 0.08f);
        emLastDesPosition = new Vector3(0.00f, 0.60f, 0.22f);
        for (int i = 13; i < 16; i++)
        {
            SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, emfirstMoveSpeed, emlastMoveSpeed, isCanLook, i);
            yield return wait_1_Second;
        }

        StartCoroutine(Co_CheckNumEm10_01());
    }

    IEnumerator Co_CheckNumEm10_01()
    {
        while (true)
        {
            if (RemainEnemies <= 0)
            {
                Phase10_01_EMDie.Invoke();
                break;
            }
            yield return null;
        }
    }

    // em 0030 4기를 생성
    public void Phase11_01()
    {

    }
}

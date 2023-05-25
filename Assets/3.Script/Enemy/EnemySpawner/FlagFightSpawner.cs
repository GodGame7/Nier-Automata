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
    private Vector3 RotateAxis;
    private float lastMoveSpeed;
    private bool isCanLook;

    // 적이 없으면 이벤트를 발생시킴
    public int RemainEnemies;

    #region 발생시킬 이벤트들
    public UnityEvent phase1_15_EMDie; // phase2 시작
    public UnityEvent phase2_01_EMDie; // phase3 시작
    public UnityEvent phase3_04_EMDie; // phase4 시작 
    public UnityEvent phase4_01_EMDie; // Phase5 시작

    #endregion

    // WaitForSeconds 모음
    private WaitForSeconds wait_half_Second = new WaitForSeconds(0.5f);



    private void Awake()
    {
        #region 이벤트 add
        flagFightSubTitleManager.phase1_15.AddListener(Phase1_15);
        flagFightSubTitleManager.phase2_01.AddListener(Phase2_01);
        flagFightSubTitleManager.phase3_04.AddListener(Phase3_04);
        flagFightSubTitleManager.phase4_01.AddListener(Phase4_01);

        #endregion
    }

    // Em0030 생성 메소드
    private void SpawnEm0030(Vector3 emRotation, Vector3 emPosition, Vector3 emFirstDesPosition, Vector3 emLastDesPosition, bool isCanLook, int num)
    {
        em0030s[num].SetActive(true);
        em0030s[num].transform.rotation = Quaternion.Euler(emRotation);
        em0030s[num].transform.position = emPosition;
        Em0030Movement em0030Movement = em0030s[num].GetComponent<Em0030Movement>();
        em0030Movement.firstDesPos = emFirstDesPosition;
        em0030Movement.lastDesPos = emLastDesPosition;
        em0030Movement.isCanLook = isCanLook;

        //05.26 병현이 여기를 부탁한다...
    }

    // Em0032 생성 메소드
    private void SpawnEm0032(Vector3 emRotation, Vector3 emPosition, Vector3 emFirstDesPosition, Vector3 emRotatePoint, Vector3 RotateAxis, float lastMoveSpeed, bool isCanLook, int num)
    {
        em0032s[num].SetActive(true);
        em0032s[num].transform.rotation = Quaternion.Euler(emRotation);
        em0032s[num].transform.position = emPosition;
        Em0032Movement em0032Movement = em0032s[num].GetComponent<Em0032Movement>();
        em0032Movement.firstDesPos = emFirstDesPosition;
        em0032Movement.RotatePoint = emRotatePoint;
        em0032Movement.RotateAxis = RotateAxis;
        em0032Movement.lastMoveSpeed = lastMoveSpeed;
        em0032Movement.isCanLook = isCanLook;
    }


    // em0030 4기를 생성
    public void Phase1_15()
    {
        RemainEnemies = 4;
        emRotation = Vector3.zero;
        isCanLook = true;

        emPosition = new Vector3(-0.20f, 0.00f, 0.30f);
        emFirstDesPosition = new Vector3(-0.20f, 0.00f, 0.10f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, isCanLook, 0);

        emPosition = new Vector3(-0.10f, 0.00f, 0.30f);
        emFirstDesPosition = new Vector3(-0.10f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, isCanLook, 1);

        emPosition = new Vector3(0.10f, 0.00f, 0.30f);
        emFirstDesPosition = new Vector3(0.10f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, isCanLook, 2);

        emPosition = new Vector3(0.20f, 0.00f, 0.30f);
        emFirstDesPosition = new Vector3(0.20f, 0.00f, 0.10f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, isCanLook, 3);

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

    // em0032 10기를 생성
    public void Phase2_01()
    {
        RemainEnemies = 10;
        emRotation = Vector3.zero;
        RotateAxis = Vector3.up;
        lastMoveSpeed = 10.00f;
        isCanLook = true;

        StartCoroutine(Co_Phase2_01());
    }

    IEnumerator Co_Phase2_01()
    {
        for (int i = 0; i < 10; i += 2)
        {
            emPosition = new Vector3(-0.30f, 0.00f, 0.30f);
            emFirstDesPosition = new Vector3(-0.20f, 0.00f, 0.15f);
            emRotatePoint = new Vector3(0.30f, 0.00f, 0.30f);
            RotateAxis = Vector3.down;
            SpawnEm0032(emRotation, emPosition, emFirstDesPosition, emRotatePoint, RotateAxis, lastMoveSpeed, isCanLook, i);
            yield return wait_half_Second;

            emPosition = new Vector3(0.30f, 0.00f, 0.30f);
            emFirstDesPosition = new Vector3(0.20f, 0.00f, 0.15f);
            emRotatePoint = new Vector3(-0.30f, 0.00f, 0.30f);
            RotateAxis = Vector3.up;
            SpawnEm0032(emRotation, emPosition, emFirstDesPosition, emRotatePoint, RotateAxis, lastMoveSpeed, isCanLook, i + 1);
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

    // em0030 6기를 생성
    public void Phase3_04()
    {
        RemainEnemies = 6;
        emRotation = Vector3.zero;
        isCanLook = true;

        StartCoroutine(Co_Phase3_04());
    }

    IEnumerator Co_Phase3_04()
    {
        for (int i = 0; i < 6; i += 2)
        {
            emPosition = new Vector3(-0.30f, 0.00f, 0.25f);
            emFirstDesPosition = new Vector3(-0.27f, 0.00f, 0.22f);
            emLastDesPosition = new Vector3(0.40f, 0.00f, 0.00f);
            SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, isCanLook, i);
            yield return wait_half_Second;

            emPosition = new Vector3(0.30f, 0.00f, 0.25f);
            emFirstDesPosition = new Vector3(0.27f, 0.00f, 0.22f);
            emLastDesPosition = new Vector3(-0.40f, 0.00f, 0.00f);
            SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emLastDesPosition, isCanLook, i+1);
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

    // em0030 4기를 생성
    public void Phase4_01()
    {
        RemainEnemies = 4;
        emRotation = Vector3.zero;
        isCanLook = true;

        emPosition = new Vector3(-0.10f, 0.00f, -0.30f);
        emFirstDesPosition = new Vector3(-0.20f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, isCanLook, 0);

        emPosition = new Vector3(-0.10f, 0.00f, -0.30f);
        emFirstDesPosition = new Vector3(-0.10f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, isCanLook, 1);

        emPosition = new Vector3(0.10f, 0.00f, -0.30f);
        emFirstDesPosition = new Vector3(0.10f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, isCanLook, 2);

        emPosition = new Vector3(0.10f, 0.00f, -0.30f);
        emFirstDesPosition = new Vector3(0.20f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, isCanLook, 3);

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


}

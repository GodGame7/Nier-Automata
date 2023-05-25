using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagFightSpawner : MonoBehaviour
{
    [Header("Enemy�� �־��ּ���.")]
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

    // ���� ������ �̺�Ʈ�� �߻���Ŵ
    public int RemainEnemies;

    #region �߻���ų �̺�Ʈ��
    public UnityEvent phase1_15_EMDie; // Laser�� �߻� 7E�� ����. 4���� ��� ���
    public UnityEvent phase2_01_EMDie; // 4�� �� Laser�� �߻� 6O�� ����. 8���� ��� ���

    #endregion

    // WaitForSeconds ����
    private WaitForSeconds wait_half_Second = new WaitForSeconds(0.5f);



    private void Awake()
    {
        #region �̺�Ʈ add
        flagFightSubTitleManager.phase1_15.AddListener(Phase1_15);
        flagFightSubTitleManager.phase2_01.AddListener(Phase2_01);
        #endregion
    }

    // Em0030 ���� �޼ҵ�
    private void SpawnEm0030(Vector3 emRotation, Vector3 emPosition, Vector3 emFirstDesPosition, Vector3 emLastDesPosition, bool isCanLook, int num)
    {
        em0030s[num].SetActive(true);
        em0030s[num].transform.rotation = Quaternion.Euler(emRotation);
        em0030s[num].transform.position = emPosition;
        Em0030Movement em0030Movement = em0030s[num].GetComponent<Em0030Movement>();
        em0030Movement.firstDesPos = emFirstDesPosition;
        em0030Movement.lastDesPos = emLastDesPosition;
        em0030Movement.isCanLook = isCanLook;
    }

    // Em0032 ���� �޼ҵ�
    private void SpawnEm0032(Vector3 emRotation, Vector3 emPosition, Vector3 emFirstDesPosition, Vector3 emRotatePoint, Vector3 RotateAxis, float lastMoveSpeed, bool isCanLook, int num)
    {
        em0032s[num].SetActive(true);
        em0032s[num].transform.rotation = Quaternion.Euler(emRotation);
        em0032s[num].transform.position = emPosition;
        Em0032Movement em0032Movement = em0032s[num].GetComponent<Em0032Movement>();
        em0032Movement.firstDesPos= emFirstDesPosition;
        em0032Movement.RotatePoint = emRotatePoint;
        em0032Movement.RotateAxis = RotateAxis;
        em0032Movement.lastMoveSpeed = lastMoveSpeed;
        em0032Movement.isCanLook = isCanLook;
    }


    // em0030 4�⸦ ����
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
            if(RemainEnemies <= 0)
            {
                phase1_15_EMDie.Invoke();
                break;
            }
            yield return null;
        }
    }    

    // em0032 10�⸦ ����
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
            SpawnEm0032(emRotation, emPosition, emFirstDesPosition, emRotatePoint, RotateAxis, lastMoveSpeed, isCanLook, i+1);
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
}

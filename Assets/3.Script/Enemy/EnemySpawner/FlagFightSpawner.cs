using System.Collections;
using System.Collections.Generic;
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
    private bool isCanLook;

    // 적이 없으면 이벤트를 발생시킴
    public int RemainEnemies;

    #region 발생시킬 이벤트들
    public UnityEvent phase1_15_EMDie; // Laser를 발사 7E를 죽임. 4초후 대사 출력

    #endregion


    private void Awake()
    {
        flagFightSubTitleManager.phase1_15.AddListener(Phase1_15);
    }

    private void SpawnEm0030(Vector3 emRotation, Vector3 emPosition, Vector3 emFirstDesPosition, Vector3 emLastDesPosition, bool isCanLook, int num)
    {
        em0030s[num].SetActive(true);
        em0030s[num].transform.position = emPosition;
        em0030s[num].transform.rotation = Quaternion.Euler(emRotation);
        Em0030Movement em0030Movement = em0030s[num].GetComponent<Em0030Movement>();
        em0030Movement.firstDesPos = emFirstDesPosition;
        em0030Movement.lastDesPos = emLastDesPosition;
        em0030Movement.isCanLook = isCanLook;
    }

    private void SpawnEm0032(Vector3 emRotation, Vector3 emPosition, Vector3 emFirstDesPosition, Vector3 emRotatePoint, Vector3 RotateAxis, bool isCanLook, int num)
    {
       // 05.24 
    }


    // em0030 4기를 생성
    public void Phase1_15()
    {
        RemainEnemies = 4;
        emRotation = Vector3.zero;

        emPosition = new Vector3(-0.20f, 0.00f, 0.20f);
        emFirstDesPosition = new Vector3(-0.20f, 0.00f, 0.10f);
        isCanLook = true;
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, isCanLook, 0);

        emPosition = new Vector3(-0.10f, 0.00f, 0.20f);
        emFirstDesPosition = new Vector3(-0.10f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, isCanLook, 1);

        emPosition = new Vector3(0.10f, 0.00f, 0.20f);
        emFirstDesPosition = new Vector3(0.10f, 0.00f, 0.15f);
        SpawnEm0030(emRotation, emPosition, emFirstDesPosition, emFirstDesPosition, isCanLook, 2);

        emPosition = new Vector3(0.20f, 0.00f, 0.20f);
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

    // em0032 10기를 생성
    public void Phase2_01()
    {
        RemainEnemies = 4;
        emRotation = Vector3.zero;
    }
}

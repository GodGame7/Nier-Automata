﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FlagFightSubTitleManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] FlagFightManager flagFightManager;
    [SerializeField] FlagFightSpawner flagFightSpawner;

    [Space(0.5f)]
    [Header("확인용")]
    [SerializeField] Text text_Subtitle;
    [SerializeField] int subTitleCounter;

    #region 발생시킬 이벤트들
    public UnityEvent phase1_01; // BackGround02 서서히 페이드 아웃
    public UnityEvent phase1_02; // player 등장
    public UnityEvent phase1_03; // BGM 시작
    public UnityEvent phase1_04; // 사령부 UI 등장
    public UnityEvent phase1_05; // 아군 Flag 등장
    public UnityEvent phase1_06; // Flag 이름 UI 등장
    public UnityEvent phase1_07; // Laser 발사, 아군 기체 이동
    public UnityEvent phase1_08; // 아군 기체 2번째 이동
    public UnityEvent phase1_09; // WASD UI 켜짐, BackGround01 서서히 꺼짐
    public UnityEvent phase1_10; // 플레이어 이동 가능
    public UnityEvent phase1_11; // 두번째 Laser 발사
    public UnityEvent phase1_12; // 카메라 이동, 플레이어 회전 및 이동, BackGround01 서서히 켜짐
    public UnityEvent phase1_13; // 안개 제거
    public UnityEvent phase1_14; // 사령부 UI 등장
    public UnityEvent phase1_15; // 사격 UI 활성화, 사격 활성화 em0030 4기 활성화 BackGround01 서서히 꺼짐

    public UnityEvent phase2_01; // 회피 UI 활성화, 회피 활성화, em0032 10기 활성화

    public UnityEvent phase3_01; // 6E에게 Laser 발사 
    public UnityEvent phase3_02; // Background1 천천히 활성화 위치 변경
    public UnityEvent phase3_03; // 플레이어 색깔변경
    public UnityEvent phase3_04; // em0030 6기 활성화, 강공격 UI 활성화, 강공격 활성화

    public UnityEvent phase4_01; // em0040 4기 활성화

    #endregion
    // WaitForSeconds 모음
    private WaitForSeconds wait_half_Second = new WaitForSeconds(0.5f);
    private WaitForSeconds wait_1_Second = new WaitForSeconds(1.0f);
    private WaitForSeconds wait_1half_Second = new WaitForSeconds(1.5f);
    private WaitForSeconds wait_2_Second = new WaitForSeconds(2.0f);
    private WaitForSeconds wait_3_Second = new WaitForSeconds(3.0f);
    private WaitForSeconds wait_4_Second = new WaitForSeconds(4.0f);
    private WaitForSeconds wait_6_Second = new WaitForSeconds(6.0f);
    private WaitForSeconds wait_7_Second = new WaitForSeconds(7.0f);



    string[] flagSubTitles = new string[]
    {
        #region phase01 23개
        "모든 존재는 사라지도록 설계되어 있다.",
        "생과 사를 되풀이 하는 나선에⋯⋯",
        "우리는 얽매여 있다.",
        "이것은 저주인가",
        "아니면 벌인가",
        "이해할 수 없는 퍼즐을 넘겨준 신에게",
        "언젠가, 우리는 반역할 수 있을까?",
        "여긴 사령부, 요르하 부대 응답하십시오.",
        "여긴 2B, 모든 기체 무사히 성층권을 돌파",
        "자동 항행 시스템에 문제없음",
        "여긴 오퍼레이터 6O, 모든 기체 반응 확인했습니다.",
        "현재, 목표로부터 50km 지점을 통과",
        "적의 방공권 내에 돌입 후, 매뉴얼 공격 형태로 이행하고",
        "목표인 대형 병기 파괴와 정보 소집을 진행해 주십시오",
        "알겠다.",
        "12H, 로스트",
        "모든 기체 매뉴얼 모드 기동, 눈으로 회피",
        "이미 기동, 이동 조작 가능",
        "장거리 레이저 발사점을 확인",
        "11B, 로스트",
        "장비 Ho229의 캔셀러 효과 없음",
        "전방에 적 기체 확인",
        "화기 사용을 신청",
        "화기 사용을 허가합니다.",
        #endregion

        #region phase02 1개
        "7E 로스트",

        #endregion

        #region phase03 1개
        "⋯⋯1D 로스트, 규정에 따라 본 기체 2B가 대장 임무를 계승"

        #endregion#
    };

    private void Awake()
    {
        text_Subtitle = GetComponentInChildren<Text>();
        subTitleCounter = 0;

        #region 이벤트 추가
        flagFightManager.phase1.AddListener(Phase01);
        flagFightSpawner.phase1_15_EMDie.AddListener(Phase02);
        flagFightSpawner.phase2_01_EMDie.AddListener(Phase03);
        flagFightSpawner.phase3_04_EMDie.AddListener(Phase04);
        #endregion
    }

    private void Phase01()
    {
        StartCoroutine(Phase01_Co());
    }

    IEnumerator Phase01_Co()
    {
        yield return wait_4_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_1_Second;
        Next_SubText();

        yield return wait_2_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_1_Second;
        Next_SubText();

        yield return wait_3_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_2_Second;
        phase1_01.Invoke();
        Next_SubText();

        yield return wait_1_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_half_Second;
        Next_SubText();

        yield return wait_1half_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_1_Second;
        phase1_02.Invoke();
        Next_SubText();

        yield return wait_4_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_1_Second;
        Next_SubText();

        yield return wait_2_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_2_Second;
        phase1_03.Invoke();

        yield return wait_4_Second;
        phase1_04.Invoke();

        yield return wait_2_Second;
        phase1_05.Invoke();
        Next_SubText();

        yield return wait_2_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_2_Second;
        Next_SubText();

        yield return wait_2_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_1_Second;
        Next_SubText();

        yield return wait_2_Second;
        text_Subtitle.gameObject.SetActive(false);
        phase1_06.Invoke();

        yield return wait_2_Second;
        Next_SubText();

        yield return wait_2_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_1_Second;
        Next_SubText();

        yield return wait_3_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_2_Second;
        Next_SubText();

        yield return wait_4_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_1_Second;
        Next_SubText();

        yield return wait_3_Second;
        phase1_07.Invoke();
        Next_SubText();

        yield return wait_1_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_6_Second;
        Next_SubText();

        yield return wait_1_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_1_Second;
        Next_SubText();

        yield return wait_2_Second;
        phase1_08.Invoke();
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_2_Second;
        Next_SubText();

        yield return wait_1_Second;
        phase1_09.Invoke();
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_2_Second;
        phase1_10.Invoke();
        Next_SubText();

        yield return wait_2_Second;
        phase1_11.Invoke();
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_7_Second;
        Next_SubText();

        yield return wait_1_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_1_Second;
        Next_SubText();

        yield return wait_2_Second;
        phase1_12.Invoke();
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_7_Second;
        phase1_13.Invoke();
        Next_SubText();

        yield return wait_4_Second;
        phase1_14.Invoke();

        yield return wait_1_Second;
        Next_SubText();

        yield return wait_2_Second;
        Next_SubText();

        yield return wait_1_Second;
        text_Subtitle.gameObject.SetActive(false);

        yield return wait_1_Second;
        phase1_15.Invoke();
    }

    private void Phase02()
    {
        StartCoroutine(Phase02_Co());
    }

    IEnumerator Phase02_Co()
    {
        yield return wait_4_Second;
        Next_SubText();

        yield return wait_2_Second;
        text_Subtitle.gameObject.SetActive(false);
        phase2_01.Invoke();
    }

    private void Phase03()
    {
        StartCoroutine(Phase03_Co());
    }

    IEnumerator Phase03_Co()
    {
        yield return wait_4_Second;
        phase3_01.Invoke();

        yield return wait_4_Second;
        Next_SubText();

        yield return wait_3_Second;
        text_Subtitle.gameObject.SetActive(false);
        phase3_02.Invoke();

        yield return wait_3_Second;
        phase3_03.Invoke();

        yield return wait_3_Second;
        phase3_04.Invoke();
    }

    private void Next_SubText()
    {
        subTitleCounter++;
        text_Subtitle.text = flagSubTitles[subTitleCounter];
        text_Subtitle.gameObject.SetActive(true);
    }

    private void Phase04()
    {
        StartCoroutine(Phase04_Co());
    }

    IEnumerator Phase04_Co()
    {
        yield return wait_4_Second;
        phase4_01.Invoke();
        
    }

}

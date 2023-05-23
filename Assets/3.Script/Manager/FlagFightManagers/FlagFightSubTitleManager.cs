using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagFightSubTitleManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] FlagFightManager flagFightManager;

    [Space(0.5f)]
    [Header("확인용")]
    [SerializeField] Text text_Subtitle;
    [SerializeField] int subTitleCounter;

    //
    private WaitForSeconds wait_1_Second = new WaitForSeconds(1.0f);
    private WaitForSeconds wait_2_Second = new WaitForSeconds(2.0f);
    private WaitForSeconds wait_3_Second = new WaitForSeconds(3.0f);


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
        "12H, 로스트",
        "모든 기체 매뉴얼 모드 기동, 눈으로 회피",
        "이미 기동, 이동 조작 가능",
        "장거리 레이저 발사점을 확인",
        "11B, 로스트",
        "장비 Ho229의 캔셀러 효과 없음",
        "전방에 적 기체 확인",
        "화기 사용을 신청",
        "화기 사용을 허가합니다."
    #endregion
    };

    private void Awake()
    {
        text_Subtitle = GetComponent<Text>();
        subTitleCounter = 0;

        #region 이벤트 추가
        flagFightManager.phase1.AddListener(Phase01);
        #endregion
    }

    private void Phase01()
    {
        StartCoroutine(Phase01_Co());
    }

    IEnumerator Phase01_Co()
    {
        yield return wait_3_Second;
        gameObject.SetActive(false);
        yield return wait_1_Second;
        Next_SubText();
        gameObject.SetActive(true);
        yield return wait_3_Second;
    }



    private void Next_SubText()
    {
        subTitleCounter++;
        text_Subtitle.text = flagSubTitles[subTitleCounter];
    }

}

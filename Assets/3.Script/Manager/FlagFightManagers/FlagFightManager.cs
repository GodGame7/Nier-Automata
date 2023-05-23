using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagFightManager : MonoBehaviour
{
    // Unity 이벤트 정의
    public UnityEvent phase1;


    private void Awake()
    {
        //
    }

    private void Start()
    {
        phase1.Invoke();
    }
    
    // phase 설명. 각 대사의 나래이션 추가할지의 여부 미정. 추후 변경될 수 있음.
    #region phase01
    //    phase01
    //
    //    BGM1 한번 시작
    //    UI SubTitle 수정
    //    모든 존재는...

    //UI SubTitle 수정
    //    생과사...

    //UI SubTitle 수정
    //    우리는...

    //UI BackGround 서서히 비활성화
    //UI SubTitle 수정
    //    이것은 저주인가

    //FlagSound 재생
    //Player 등장
    //UI SubTitle 수정
    //    아니면 벌인가

    //UI SubTitle 수정
    //    이해 ㄴㄴ

    //UI SubTitle 수정
    //    언젠가

    //BGM 변경

    //사령부 UI 등장
    //등장 Sound 재생

    //UI SubTitle 수정
    //    여기는 사령부
    //아군 기체 등장

    //UI SubTitle 수정
    //    여긴 2B

    //UI SubTitle 수정
    //    자동 문제 ㄴㄴ

    //아군 기체 이름 UI 켜짐
    //UI SubTitle 수정
    //    여긴 6O

    //아군 기체 이름 UI 꺼짐
    //현재, 50

    //UI SubTitle 수정
    //적의 방공권

    //UI SubTitle 수정
    //목표인

    //Laser 발사
    //기체 이동
    //UI SubTitle 수정
    //알겠

    //UI SubTitle 수정
    //    12H

    //UI SubTitle 수정
    //    눈으로 회피

    //UI SubTitle 수정
    //이미 기동,
    //기체 좌우 이동

    //WASD UI 켜짐
    //UP DOWN 서서히 꺼짐

    //UI SubTitle 수정
    //장거리
    //Player 움직임 가능

    //Laser

    //UI SubTitle 수정
    //11B

    //장비 Ho229

    //UP DOWN 서서히 켜짐
    //카메라 movement
    //플레이어 회전 및 이동

    //fog of

    //UI SubTitle 수정
    //전방에 적 기체 확인

    //UI SubTitle 수정
    //화기 사용을 신청

    //사령부 UI 등장
    //등장 Sound 재생
    //UI SubTitle 수정
    //화기 사용을 허가

    //Enemy 0030 4기 등장
    //사격 UI OPEN
    //사격 On
    #endregion













}

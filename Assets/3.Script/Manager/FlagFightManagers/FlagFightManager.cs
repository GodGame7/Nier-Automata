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
    /* phase01

02:36
UI SubTitle 수정
    모든 존재는...

02:40
UI SubTitle 비활성화	

02:41
UI SubTitle 수정
    생과사...

02:43
UI SubTitle 비활성화	

02:44
UI SubTitle 수정
    우리는...

02:47
UI SubTitle 비활성화	

02:49
UI BackGround 서서히 비활성화
UI SubTitle 수정
    이것은 저주인가

02:50
UI SubTitle 비활성화	

02:50.5
UI SubTitle 수정
    아니면 벌인가
Player 등장

02:52
UI SubTitle 비활성화	

02:53
UI SubTitle 수정
    이해 ㄴㄴ

02:57
UI SubTitle 비활성화	

02:58
UI SubTitle 수정
    언젠가

03:00
UI SubTitle 비활성화	
BGM 활성화

03:06
사령부 UI 등장

03:08
UI SubTitle 수정
    여기는 사령부
아군 기체 등장

03:10
UI SubTitle 비활성화	

03:12
UI SubTitle 수정
    여긴 2B

03:13.5
UI SubTitle 비활성화	

03:14
UI SubTitle 수정
    자동 문제 ㄴㄴ

03:16
UI SubTitle 비활성화	
아군 기체 이름 UI 하나씩 켜짐

03:18
UI SubTitle 수정
    여긴 6O

03:20
UI SubTitle 비활성화	

03:21
아군 기체 이름 UI 꺼짐
현재, 50

03:24
UI SubTitle 비활성화	

03:26
UI SubTitle 수정
적의 방공권

03:30
UI SubTitle 비활성화	

03:31
UI SubTitle 수정
목표인

03:34
Laser 발사
아군 기체 이동
UI SubTitle 수정
알겠

03:35
UI SubTitle 비활성화	

(03:36 Laser 맞음)

03:41
UI SubTitle 수정
12H

03:42
UI SubTitle 비활성화	

03:43
UI SubTitle 수정
눈으로 회피

03:45
UI SubTitle 비활성화	
아군 기체 이동

03:47
UI SubTitle 수정
이미 기동,
아군 기체 좌우 이동

03:48
UI SubTitle 비활성화
WASD UI 켜짐
UP DOWN 서서히 꺼짐

03:50
UI SubTitle 수정
장거리
Player 움직임 가능

03:52
UI SubTitle 비활성화
Laser

03:59
UI SubTitle 수정
11B

04:00
UI SubTitle 비활성화

04:01
UI SubTitle 수정
장비 Ho229

04:03
UI SubTitle 비활성화
UP DOWN 서서히 켜짐
카메라 movement
플레이어 이동 및 회전

04:10
UI SubTitle 수정
전방에 적 기체 확인
fog off

04:14
사령부 UI 등장

04:15
UI SubTitle 수정
화기 사용을 신청

04:17
UI SubTitle 수정
화기 사용을 허가

04:18
UI SubTitle 비활성화

04:19
사격 UI 활성화
사격 활성화
em0030 4마리 활성화
*/
    #endregion
    #region phase02
    /*
     //phase2

04:00 phase1_15_EMDie
Laser를 발사 7E를 죽임. 4초후 대사 출력

04:04
UI SubTitle 수정 및 출력
   7E

04:06
UI SubTitle 비활성화	
회피 UI 활성화
회피 활성화
em0032 10기 활성화
    */
    #endregion













}

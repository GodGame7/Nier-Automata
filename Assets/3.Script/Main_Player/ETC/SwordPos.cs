using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPos : MonoBehaviour
{
    [SerializeField] public Transform leftHand;
    [SerializeField]public Transform rightHand;
    [SerializeField] public Transform staff;
    private void Start()
    {
        leftHand = Main_Player.Instance.leftHand;
        rightHand = Main_Player.Instance.rightHand;;
    }
    private void LateUpdate()
    {
        // 왼손과 오른손 중심점 계산
        Vector3 handsCenter = (leftHand.position + rightHand.position) * 0.5f;

        // 봉의 위치를 손 중심점에 배치
        staff.position = handsCenter;

        // 봉의 회전을 두 손의 방향에 맞추기
        Vector3 direction = rightHand.position - leftHand.position;
        staff.rotation = Quaternion.LookRotation(direction);
    }
}

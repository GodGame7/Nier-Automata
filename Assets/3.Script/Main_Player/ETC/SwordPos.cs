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
        // �޼հ� ������ �߽��� ���
        Vector3 handsCenter = (leftHand.position + rightHand.position) * 0.5f;

        // ���� ��ġ�� �� �߽����� ��ġ
        staff.position = handsCenter;

        // ���� ȸ���� �� ���� ���⿡ ���߱�
        Vector3 direction = rightHand.position - leftHand.position;
        staff.rotation = Quaternion.LookRotation(direction);
    }
}

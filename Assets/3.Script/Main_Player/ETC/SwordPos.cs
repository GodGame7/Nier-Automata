using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPos : MonoBehaviour
{
    [SerializeField] public Transform leftHand;
    [SerializeField]public Transform rightHand;
    [SerializeField] public Transform staff;
    public bool isHand;
    private void Start()
    {
        isHand = true;
        leftHand = Main_Player.Instance.leftHand;
        rightHand = Main_Player.Instance.rightHand;;
    }

    public void Load()
    {
        isHand=true;
        gameObject.SetActive(true);
    }
    public void Reset()
    {
        isHand = false;
        gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        // 왼손과 오른손 중심점 계산
        //Vector3 handsCenter = (leftHand.position + rightHand.position) * 0.5f;

        if (isHand) { 
        staff.position = rightHand.position;
        staff.rotation = rightHand.rotation;
        }

        // Rotate
        //Vector3 direction = rightHand.position - leftHand.position;
        //staff.rotation = Quaternion.LookRotation(direction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSwordPos : MonoBehaviour
{
    [SerializeField] public Transform leftHand;
    [SerializeField] public Transform rightHand;
    [SerializeField] public Transform staff;
    public bool isHand;
    private void Start()
    {
        isHand = true;
        leftHand = Main_Player.Instance.leftHand;
        rightHand = Main_Player.Instance.rightHand;
    }

    public void Load()
    {
        isHand = true;
        gameObject.SetActive(true);
    }
    public void Reset()
    {
        isHand = false;
        gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (isHand)
        {
            staff.position = rightHand.position;
            staff.rotation = rightHand.rotation;
        }

    }
}

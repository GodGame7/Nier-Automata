using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserCoolTime : MonoBehaviour
{
    [SerializeField] GameObject Pord;
    [SerializeField] Text CoolTime_text;
    [SerializeField] Slider CoolTime_Slider;
    [SerializeField] Camera MainCam;
    [SerializeField]
    private Vector3 CoolPos = new Vector3(0, -0.5f, 0f);
    private float LaserCool = 7f;
    private float CurrentCool = 0f;
    public bool CanLaser = true;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable() // ��������� ��Ÿ�� ����
    {
        CurrentCool = LaserCool;
    }
    private void Update()
    {

        if (CurrentCool > 0f) //��Ÿ���� 0���̻��Ͻ� ������ �ٽ� �����,��Ÿ���� �پ�鵵��
        {
            CurrentCool -= Time.deltaTime;
            CanLaser = false;
        }
        else //��Ÿ���� 0�����϶�� ����ֵ���, ��Ÿ��UI ��Ȱ��ȭ
        {
            CanLaser = true;
            gameObject.SetActive(false);
        }
        //CoolPos = (Pord.transform.rotation.eulerAngles);
        Vector3 targetPos = Pord.transform.position + -0.5f*Pord.transform.up - Pord.transform.forward * 1f;
        //if (Mathf.Abs(MainCam.transform.rotation.eulerAngles.y)>= 90 && Mathf.Abs(MainCam.transform.rotation.eulerAngles.y) <= 180)
        //{
        //    targetPos.z -= -1;
        //}
        transform.position = targetPos;
        //transform.rotation = Pord.transform.rotation;
        CoolTime_text.text = string.Format("{0:##0}", CurrentCool * 100f);
        CoolTime_Slider.value = CurrentCool / LaserCool;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordBulletControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //todo ��μ� 0518 ������ �ִ°� ��������� ���ʹ̿� ������ �����̶� �����Ϸ�
            transform.position = new Vector3(999, 999, 999);
            gameObject.SetActive(false);
        }

    }

   

    
}

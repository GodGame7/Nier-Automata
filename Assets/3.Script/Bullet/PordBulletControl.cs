using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordBulletControl : MonoBehaviour
{
    private Vector3 WaitLocation = new Vector3(999, 999, 999);
    private void OnEnable()
    {
        StartCoroutine(Disable());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //todo ��μ� 0518 ������ �ִ°� ��������� ���ʹ̿� ������ �����̶� �����Ϸ�
            transform.position = WaitLocation;
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Wall"))
        {
            transform.position = WaitLocation;
            gameObject.SetActive(false);
        }

    }
    
    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
        


}

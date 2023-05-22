using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBullet : MonoBehaviour
{
    private float moveSpeed = 0.6f;


    private void FixedUpdate()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(TryGetComponent(out FlagEmInformation enemy))
            {
                enemy.OnDamage(PlayerData.instance.atk);
            }
            gameObject.SetActive(false);
        }
        else if(other.CompareTag("BulletSoft"))
        {
            // todo �� �Ѿ� ��Ȱ��ȭ���� �ı�����
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

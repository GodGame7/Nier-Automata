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
            if(TryGetComponent(out Em0032Movement enemy))
            {
                // todo 데미지 플레이어 데이터 받아와서 설정할 것
                enemy.OnDamage(1);
            }
            gameObject.SetActive(false);
        }
        else if(other.CompareTag("BulletSoft"))
        {
            // todo 적 총알 비활성화인지 파괴인지
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

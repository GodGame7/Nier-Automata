using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out EnemyHp eh);
        Debug.Log("충돌");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("데미지");
            eh.TakeDamage(2);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out EnemyHp eh);
        Debug.Log("�浹");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("������");
            eh.TakeDamage(2);
        }
    }
}

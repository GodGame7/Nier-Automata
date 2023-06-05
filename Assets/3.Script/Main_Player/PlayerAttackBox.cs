using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("적과충돌");
            other.transform.root.GetComponent<EnemyHp>().TakeDamage(Random.Range(12,18));
        }
        else if (other.CompareTag("BulletSoft"))
        {
            Debug.Log("총알과충돌");
            other.gameObject.SetActive(false);
        }
    }
}

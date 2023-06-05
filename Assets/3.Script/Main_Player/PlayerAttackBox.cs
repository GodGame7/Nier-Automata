using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    public ParticleSystem hiteffect;
    public ParticleSystem hiteffect2;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("적과충돌");
            other.transform.root.GetComponent<EnemyHp>().TakeDamage(Random.Range(12,18));
            //히트이펙트 위치 잡아주기
            Vector3 effectpos = other.ClosestPoint(transform.position);
            hiteffect.transform.position = effectpos;
            hiteffect.Play();
        }
        else if (other.CompareTag("BulletSoft"))
        {
            Debug.Log("총알과충돌");
            other.gameObject.SetActive(false);
            Vector3 effectpos = other.ClosestPoint(transform.position);
            hiteffect2.transform.position = effectpos;
            hiteffect2.Play();
        }
    }
}

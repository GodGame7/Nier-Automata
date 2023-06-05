using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    public ParticleSystem hiteffect;
    public ParticleSystem hiteffect2;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�浹");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("�����浹");
            other.transform.root.GetComponent<EnemyHp>().TakeDamage(Random.Range(12,18));
            //��Ʈ����Ʈ ��ġ ����ֱ�
            Vector3 effectpos = other.ClosestPoint(transform.position);
            hiteffect.transform.position = effectpos;
            hiteffect.Play();
        }
        else if (other.CompareTag("BulletSoft"))
        {
            Debug.Log("�Ѿ˰��浹");
            other.gameObject.SetActive(false);
            Vector3 effectpos = other.ClosestPoint(transform.position);
            hiteffect2.transform.position = effectpos;
            hiteffect2.Play();
        }
    }
}

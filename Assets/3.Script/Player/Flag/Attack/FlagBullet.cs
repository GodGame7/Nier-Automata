using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBullet : MonoBehaviour
{
    protected float moveSpeed = 0.6f;
    protected float damage;

    private void Start()
    {
        if (PlayerData.Instance != null)
        {
            damage = PlayerData.Instance.atk;
        }
    }

    protected void FixedUpdate()
    {
        transform.position += moveSpeed * Time.deltaTime * -transform.up;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Define.EnemyTagType.Enemy.ToString()))
        {
            if(other.TryGetComponent(out FlagEmInformation enemy))
            {
                enemy.OnDamage(damage);
            }
            gameObject.SetActive(false);
        }
        else if(other.CompareTag(Define.EnemyTagType.BulletSoft.ToString()))
        {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}
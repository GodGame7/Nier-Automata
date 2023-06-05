using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemyBullet : MonoBehaviour
{
    [SerializeField] float damage = 10.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Main_Player.Instance.OnDamage(damage);
        }
    }
}

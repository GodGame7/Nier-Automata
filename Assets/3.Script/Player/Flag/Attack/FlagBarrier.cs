using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBarrier : MonoBehaviour
{
    private float meleeDamageMagnification = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out FlagEmInformation enemy))
            {
                enemy.OnDamage(PlayerData.instance.atk * meleeDamageMagnification);
            }
        }
        if (other.CompareTag("BulletSoft") || other.CompareTag("BulletHard"))
        {
            Destroy(other.gameObject);
        }
    }
}

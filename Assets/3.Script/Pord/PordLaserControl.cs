using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordLaserControl : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.transform.root.GetComponent<EnemyHp>().TakeDamage(90);
        }
    }


}

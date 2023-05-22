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
                enemy.OnDamage(1);
            }
            gameObject.SetActive(false);
        }
        else if(other.CompareTag("BulletSoft"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

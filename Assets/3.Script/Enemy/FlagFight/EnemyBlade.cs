using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlade : MonoBehaviour
{
    [SerializeField] float Damage = 20.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerData playerData))
            {
                playerData.OnDamage(Damage);
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        yield return new WaitForSeconds(1f);

        collider.enabled = true;
    }
}

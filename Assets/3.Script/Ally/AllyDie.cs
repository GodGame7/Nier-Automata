using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyDie : MonoBehaviour
{
    [Header("Ally Æø¹ß")]
    [SerializeField] GameObject Ally;
    [SerializeField] GameObject explosion;

    public void Die()
    {
        Ally.SetActive(false);
        explosion.transform.position = Ally.transform.position;
        explosion.SetActive(true);
        StartCoroutine(Co_Dying());
    }

    IEnumerator Co_Dying()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
}

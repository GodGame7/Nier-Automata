using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyDie : MonoBehaviour
{
    [Header("Ally Æø¹ß")]
    [SerializeField] GameObject Ally;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deathClip;

    public void Die()
    {
        StartCoroutine(Co_Dying());
    }

    IEnumerator Co_Dying()
    {
        explosion.transform.position = Ally.transform.position;
        yield return new WaitForSeconds(1.5f);
        if (audioSource != null && deathClip != null)
        {
            audioSource.PlayOneShot(deathClip);
        }
        Ally.SetActive(false);
        explosion.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
}

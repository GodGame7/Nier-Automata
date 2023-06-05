using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordBulletControl : MonoBehaviour
{
    private Vector3 WaitLocation = new Vector3(999, 999, 999);
    [SerializeField] ParticleSystem Spark;
    private void Awake()
    {
        Spark = Instantiate(Spark, transform.position, Quaternion.identity);
        Spark.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(Disable());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.transform.root.GetComponent<EnemyHp>().TakeDamage(6);
            transform.position = WaitLocation;
            gameObject.SetActive(false);

            Spark.gameObject.transform.position = other.gameObject.transform.position;
            Spark.gameObject.SetActive(true);
            Spark.Play();
            Invoke("DisableSpark", 1f);
        }
        if (other.CompareTag("BulletSoft"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }


    }

    private IEnumerator Disable() //불렛 지속시간 설정
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }

    private void DisableSpark()
    {
        Spark.gameObject.SetActive(false);
    }


}

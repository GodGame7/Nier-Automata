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
        Invoke("DisableBullet", 4f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.transform.root.GetComponent<EnemyHp>().TakeDamage(6);
            transform.position = WaitLocation;
            DisableBullet();

            Spark.gameObject.transform.position = other.gameObject.transform.position;
            Spark.gameObject.SetActive(true);
            Spark.Play();
            Invoke("DisableSpark", 1f);
        }
        if (other.CompareTag("BulletSoft"))
        {
            other.gameObject.SetActive(false);
            DisableBullet();
        }
        if (other.CompareTag("Wall"))
        {
            DisableBullet();
        }

    }

   
    private void DisableBullet() //불렛 일정시간후 지우기위함
    {
        gameObject.SetActive(false);
    }
    private void DisableSpark() //파티클시스템 일정시간후 지우기 위함
    {
        Spark.gameObject.SetActive(false);
    }


}

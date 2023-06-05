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

   
    private void DisableBullet() //�ҷ� �����ð��� ���������
    {
        gameObject.SetActive(false);
    }
    private void DisableSpark() //��ƼŬ�ý��� �����ð��� ����� ����
    {
        Spark.gameObject.SetActive(false);
    }


}

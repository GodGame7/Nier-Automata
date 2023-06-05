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
            Spark.transform.position = other.gameObject.transform.position;
            Spark.Play();
        }
        if (other.CompareTag("BulletSoft"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
         

    }
    
    private IEnumerator Disable() //�ҷ� ���ӽð� ����
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
        


}

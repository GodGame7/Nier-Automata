using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class LaserControl : MonoBehaviour
{
    [Header("아군 기체 : 12H, 11B, 7E, 1D, 4B 순서")]
    [SerializeField] Transform[] Allies;

    [Header("Laser")]
    [SerializeField] GameObject GuideLaser;
    [SerializeField] GameObject Laser;
    [SerializeField] int counter;
    [SerializeField] float followSpeed;
    WaitForSeconds wait_1_secounds = new WaitForSeconds(1.5f);
    WaitForSeconds wait_2_secounds = new WaitForSeconds(3.0f);

    public void FireLaser(int num)
    {
        counter = num;
        AudioManager.Instance.PlaySfx(Define.SFX.Raser);
        StartCoroutine(Co_Follow());
        StartCoroutine(Co_Fire());
    }

    IEnumerator Co_Follow()
    {
        Vector3 targetPosition = new Vector3(Allies[counter].position.x, transform.position.y, transform.position.z);

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
            yield return null;
        }
    }
    
    IEnumerator Co_Fire()
    {
        GuideLaser.SetActive(true);
        yield return wait_1_secounds;

        GuideLaser.SetActive(false);
        Laser.SetActive(true);

        yield return wait_2_secounds;

        Laser.SetActive(false);
    }








}

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
        if(other.CompareTag("Enemy"))
        {
            //에너미 데미지 주는 부분
        }
    }
}

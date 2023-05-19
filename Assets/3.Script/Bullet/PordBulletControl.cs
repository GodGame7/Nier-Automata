using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordBulletControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //todo 김민수 0518 데미지 넣는거 여기넣을지 에너미에 넣을지 수민이랑 결정하렴
            transform.position = new Vector3(999, 999, 999);
            gameObject.SetActive(false);
        }

    }

   

    
}

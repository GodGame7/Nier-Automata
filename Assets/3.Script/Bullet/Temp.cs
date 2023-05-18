using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{

    /*

    인풋에다가 넘길거
    */
    //이거는 불렛 스폰에다가
    [SerializeField] GameObject[] Bullet;
    [SerializeField] GameObject Pord;
    [SerializeField] Camera Cam;
    private bool isLockOn;
    private bool isMonster;
    private Vector3 targetpos;

    private int bulletCount = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Bullet[bulletCount].transform.position = Pord.transform.position;
            Bullet[bulletCount].SetActive(true);
            if (isLockOn)
            {
                Bullet[bulletCount].GetComponent<BulletMovement>().Move(targetpos);
                
            }
            else
            {
                Bullet[bulletCount].GetComponent<BulletMovement>().Move(-Cam.transform.position.normalized);
                //방향 조정 필요 임시로 넣어뒀음
            }
            bulletCount++;
            if (bulletCount > 40) // BulletSpawn 에서 생성한 갯수만큼
            {
                bulletCount = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isMonster)
            {
                isLockOn = true;
            }
        }

       

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isMonster = true;

            if(isLockOn)
            {
                targetpos = other.transform.position; 
            }
        }

    }
}

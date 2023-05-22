using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordControl : MonoBehaviour
{
    [SerializeField] GameObject[] Bullet;
    [SerializeField] Camera Cam;

    // 록온을 위한 변수
    private bool isLockOn = false;
    private bool isMonster = false;

    // 록온시 타겟 위치
    private Vector3 targetpos;

    //오브젝트 풀링용 갯수
    private int bulletCount = 0;
    private void Update()
    {
        // ------------------------인풋매니저로 넘길부분 ----------------------------
        if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            Bullet[bulletCount].transform.position = transform.position;
            Bullet[bulletCount].SetActive(true);
            if (isLockOn)
            {
                Bullet[bulletCount].GetComponent<PordBulletMovement>().Move(targetpos);

            }
            else
            {
                Bullet[bulletCount].GetComponent<PordBulletMovement>().Move(-Cam.transform.position.normalized);
                //방향 조정 필요 임시로 넣어뒀음
            }
            bulletCount++;
            if (bulletCount >= 60) // BulletSpawn 에서 생성한 갯수만큼
            {
                bulletCount = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isLockOn)
            {
                isLockOn = false;
            }
            else if (isMonster)
            {
                isLockOn = true;
            }

        }

        // ----------------------------여기까지 -----------------------------

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isMonster = true;

            if (isLockOn)
            {
                targetpos = other.transform.position;
            }
        }

    }

}

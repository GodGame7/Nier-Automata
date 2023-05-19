using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordControl : MonoBehaviour
{
    [SerializeField] GameObject[] Bullet;
    [SerializeField] Camera Cam;
    private bool isLockOn = false;
    private bool isMonster = false;
    private Vector3 targetpos;

    private int bulletCount = 0;
    private void Update()
    {
        // ------------------------인풋매니저로 넘길부분 ----------------------------
        if (Input.GetKeyDown(KeyCode.RightShift))
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
            if (bulletCount >= 40) // BulletSpawn 에서 생성한 갯수만큼
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
    private void OnTriggerEnter(Collider other)
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

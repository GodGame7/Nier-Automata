using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordControl : MonoBehaviour
{
    [SerializeField] GameObject[] Bullet;
    [SerializeField] Camera Cam;

    // �Ͽ��� ���� ����
    private bool isLockOn = false;
    private bool isMonster = false;

    // �Ͽ½� Ÿ�� ��ġ
    private Vector3 targetpos;

    //������Ʈ Ǯ���� ����
    private int bulletCount = 0;
    private void Update()
    {
        // ------------------------��ǲ�Ŵ����� �ѱ�κ� ----------------------------
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
                //���� ���� �ʿ� �ӽ÷� �־����
            }
            bulletCount++;
            if (bulletCount >= 60) // BulletSpawn ���� ������ ������ŭ
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

        // ----------------------------������� -----------------------------

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

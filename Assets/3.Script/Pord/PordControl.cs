using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordControl : MonoBehaviour
{
    [SerializeField] Camera Cam;
    [SerializeField] PordBulletSpawn PordBullet;
    [SerializeField] GameObject PordLaser;

    // �Ͽ��� ���� ����
    private bool isLockOn = false;
    private bool isMonster = false;

    // �Ͽ½� Ÿ�� ��ġ
    private Vector3 targetpos;

    //������Ʈ Ǯ���� ����
    private int bulletCount = 0;

    //�Ѿ� �����̿� ����
    private float BulletDealyTime = 0.1f;
    private float CurrentTime = 0f;

    //������ �����̿� ����
    private bool isLaser = false;

    private void Update()
    {
        // ------------------------��ǲ�Ŵ����� �ѱ�κ� ----------------------------
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            CurrentTime += Time.deltaTime;
            if (BulletDealyTime > CurrentTime)
            {
                return; //������ �ð������� �Ѿ��� �����
            }
            PordBullet.Bullet[bulletCount].transform.position = transform.position;
            PordBullet.Bullet[bulletCount].SetActive(true);
            if (isLockOn)
            {
                PordBullet.Bullet[bulletCount].GetComponent<PordBulletMovement>().Move(targetpos);

            }
            else
            {
                PordBullet.Bullet[bulletCount].GetComponent<PordBulletMovement>().Move(-Cam.transform.position.normalized);
                //���� ���� �ʿ� �ӽ÷� �־����
            }

            //�Ҹ��� ���⿡ �־��ּ���

            bulletCount++;
            if (bulletCount >= 60) // BulletSpawn ���� ������ ������ŭ
            {
                bulletCount = 0;
            }
            CurrentTime = 0;
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

        if (Input.GetKeyDown(KeyCode.Alpha1) && !isLaser)
        {

            StartCoroutine(Laser_co());
            //���� �����ʿ� �ӽ÷� �־����
        }
        // ----------------------------������� -----------------------------

    }

    private IEnumerator Laser_co()
    {
        //ī�޶� ��¦ �����ּ���
        //�Ҹ��� ���⿡ �־��ּ���

        isLaser = true;
        PordLaser.SetActive(true);
        PordLaser.transform.position = transform.position;
        PordLaser.transform.rotation = Quaternion.Euler(-Cam.transform.position.normalized);
        yield return new WaitForSeconds(1f);
        PordLaser.SetActive(false);
        isLaser = false;
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

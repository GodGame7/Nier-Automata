using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordControl : MonoBehaviour
{
    [SerializeField] Camera Cam;
    [SerializeField] PordBulletSpawn PordBullet;
    [SerializeField] GameObject PordLaser;

    // 록온을 위한 변수
    private bool isLockOn = false;
    private bool isMonster = false;

    // 록온시 타겟 위치
    private Vector3 targetpos;

    //오브젝트 풀링용 갯수
    private int bulletCount = 0;

    //총알 딜레이용 변수
    private float BulletDealyTime = 0.1f;
    private float CurrentTime = 0f;

    //레이저 딜레이용 변수
    private bool isLaser = false;

    private void Update()
    {
        // ------------------------인풋매니저로 넘길부분 ----------------------------
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            CurrentTime += Time.deltaTime;
            if (BulletDealyTime > CurrentTime)
            {
                return; //딜레이 시간동안은 총알을 못쏘도록
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
                //방향 조정 필요 임시로 넣어뒀음
            }

            //소리를 여기에 넣어주세용

            bulletCount++;
            if (bulletCount >= 60) // BulletSpawn 에서 생성한 갯수만큼
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
            //방향 조정필요 임시로 넣어뒀음
        }
        // ----------------------------여기까지 -----------------------------

    }

    private IEnumerator Laser_co()
    {
        //카메라를 살짝 흔들어주세용
        //소리도 여기에 넣어주세용

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

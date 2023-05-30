using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordControl : MonoBehaviour
{
    [SerializeField] Camera Cam;
    [SerializeField] PordBulletSpawn PordBullet;
    [SerializeField] GameObject PordLaser;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Lockon;
    [SerializeField] GameObject MagicCircle;

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

    //포드의 위치를 위한 변수
    private Vector3 PlayerAround = new Vector3(1, 1, 0);
    private Vector3 TopPosition = new Vector3(0, 1.8f, 0);
    private Vector3 MagicCirclePositon = new Vector3(0, 0, 0.4f);

    //포드의 움직임 제어용 변수
    private bool isActive = false;

    private void Update()
    {
        
                Debug.Log(targetpos);

        if (transform.position.y >= (Player.transform.position + TopPosition).y && !isActive)
        {
            transform.position -= Vector3.up * 0.4f * Time.deltaTime;
        }
        else if (transform.position.y < (Player.transform.position + TopPosition).y && !isActive)
        {
            transform.position += Vector3.up * 0.2f * Time.deltaTime;
        }

        // ------------------------인풋매니저로 넘길부분 ----------------------------
        if (Input.GetKey(KeyCode.RightShift) && !isLaser ||
            Input.GetKey(KeyCode.LeftShift) && !isLaser) //레이저와 동시에 나가는것 방지
        {
            CurrentTime += Time.deltaTime;
            if (BulletDealyTime > CurrentTime)
            {
                return; //딜레이 시간동안은 총알을 못쏘도록
            }

            isActive = true;
            transform.position = Player.transform.position + PlayerAround;

            MagicCircle.transform.position = transform.position + MagicCirclePositon;
            MagicCircle.SetActive(true);

            PordBullet.Bullet[bulletCount].transform.position = transform.position + MagicCirclePositon;
            PordBullet.Bullet[bulletCount].SetActive(true);

            if (isLockOn)
            {
                PordBullet.Bullet[bulletCount].GetComponent<PordBulletMovement>().Move((targetpos-PordBullet.Bullet[bulletCount].transform.position).normalized);
                Debug.Log(targetpos);
                //PordBullet.Bullet[bulletCount].transform.LookAt(targetpos);
                //PordBullet.Bullet[bulletCount].GetComponent<PordBulletMovement>().Move(Vector3.forward);
                
                //PordBullet.Bullet[bulletCount].transform.LookAt(targetpos);
            }
            else
            {
                PordBullet.Bullet[bulletCount].GetComponent<PordBulletMovement>().Move(-Cam.transform.position.normalized);
                //방향 조정 필요 임시로 넣어뒀음
            }

            //소리를 여기에 넣어주세용
            Invoke("ActiveFalse", 0.3f);
            bulletCount++;
            if (bulletCount >= 60) // BulletSpawn 에서 생성한 갯수만큼
            {
                bulletCount = 0;
            }
            CurrentTime = 0;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) ||
            Input.GetKeyUp(KeyCode.RightShift))  // 키를 뗐을 때 마법진 사라지게
        {
            MagicCircle.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Q)) // 록온
        {

            if (isLockOn)
            {
                isLockOn = false;
                Lockon.SetActive(false);
            }
            else if (isMonster)
            {
                isLockOn = true;
                Lockon.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !isLaser)
        {

            StartCoroutine(Laser_co());
            //방향 조정필요 임시로 넣어뒀음
        }

        //-- 플레이어 움직임에 맞춰서 포드도 움직임--

        //속도 플레이어랑 맞춰주렴
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, 1) * 5f * Time.deltaTime;
            //임시값 , 추후 플레이어 이동속도랑 움직이는거보고 변경예정
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -1) * 5f * Time.deltaTime;
            //임시값 , 추후 플레이어 이동속도랑 움직이는거보고 변경예정
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0) * 5f * Time.deltaTime;
            //임시값 , 추후 플레이어 이동속도랑 움직이는거보고 변경예정
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0) * 5f * Time.deltaTime;
            //임시값 , 추후 플레이어 이동속도랑 움직이는거보고 변경예정
        }
        // ----------------------------여기까지 -----------------------------

    }

    private IEnumerator Laser_co()
    {
        //카메라를 살짝 흔들어주세용
        //소리도 여기에 넣어주세용
        transform.position = Player.transform.position + PlayerAround;
        isActive = true;
        isLaser = true;
        PordLaser.SetActive(true);
        MagicCircle.SetActive(true);
        PordLaser.transform.position = transform.position + MagicCirclePositon;
        if (isLockOn)
        {
            Debug.Log(targetpos);
            PordLaser.transform.LookAt(targetpos);
            //if(PordLaser.TryGetComponent(out LineRenderer line))
            //{
            //    Debug.Log("enter");
            //    line.SetPosition(0, Vector3.zero);
            //    line.SetPosition(1, new Vector3(-targetpos.x,targetpos.y,-targetpos.z+0.2f));
            //    //PordLaser.transform.position = Vector3.zero;
            //}
        }
        else
        {
            PordLaser.transform.rotation = Quaternion.Euler(-Cam.transform.position.normalized);

        }

        yield return new WaitForSeconds(1f);
        // 레이저가 끝난 이후 
        PordLaser.SetActive(false);
        MagicCircle.SetActive(false);
        isLaser = false;
        ActiveFalse();
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(isMonster);
        if (other.CompareTag("Enemy"))
        {
            isMonster = true;

            if (isLockOn)
            {
                targetpos = other.transform.position;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isMonster = false;
        }
    }


    public void ActiveFalse()
    {
        isActive = false;
    }
}

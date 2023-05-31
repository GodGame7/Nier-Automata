using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordControl : MonoBehaviour
{
    [Header("설정을 위해 넣어주세용")]
    [SerializeField] Camera Cam;
    [SerializeField] PordBulletSpawn PordBullet;
    [SerializeField] GameObject PordLaser;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Lockon;
    [SerializeField] GameObject MagicCircle;
    [SerializeField] LaserCoolTime LaserCoolTime;
    [SerializeField] ParticleSystem Smoke;


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

            Smoke.Play();
            Smoke.transform.position = transform.position + MagicCirclePositon;

            if (isLockOn) //록온시 타겟방향으로
            {
                PordBullet.Bullet[bulletCount].GetComponent<PordBulletMovement>().Move((targetpos-PordBullet.Bullet[bulletCount].transform.position).normalized);
            }
            else //록온이 아닐시 앞으로
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

            if (isLockOn) //록온일시 록온해제
            {
                isLockOn = false;
                Lockon.SetActive(false);
            }
            else if (isMonster) //록온이 아닐시 , 주변에 몬스터가있는(록온가능상태)라면 록온
            {
                isLockOn = true;
                Lockon.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && LaserCoolTime.CanLaser) // 1입력시 레이저 쿨타임이 아니라면
        {

            StartCoroutine(Laser_co());
            LaserCoolTime.gameObject.SetActive(true);
            
            
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

    private IEnumerator Laser_co() // 레이저 사용시 코루틴
    {
        //카메라를 살짝 흔들어주세용
        //소리도 여기에 넣어주세용
        transform.position = Player.transform.position + PlayerAround;
        isActive = true;
        isLaser = true;
        PordLaser.SetActive(true);
        MagicCircle.SetActive(true);
        PordLaser.transform.position = transform.position + MagicCirclePositon;
        if (isLockOn) //록온시 타겟 에게 방향 설정
        {
            PordLaser.transform.LookAt(targetpos);
        }
        else //록온이 아닐시 앞으로 나가게
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
    private void OnTriggerStay(Collider other) // 범위안에 들어왔을시 록온 가능설정, 록온시 타겟위치 설정
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
    private void OnTriggerExit(Collider other) // 범위 밖으로 나갈시 록온 해제
    {
        if (other.CompareTag("Enemy"))
        {
            isMonster = false;
            isLockOn = false;
            Lockon.SetActive(false);
        }
    }


    public void ActiveFalse()
    {
        isActive = false;
    }
}

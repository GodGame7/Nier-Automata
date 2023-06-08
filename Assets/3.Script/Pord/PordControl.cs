using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordControl : MonoBehaviour
{
    [Header("������ ���� �־��ּ���")]
    [SerializeField] Camera Cam;
    [Header("PordObject�� ����")]
    [Space(10)]
    [SerializeField] PordBulletSpawn PordBullet;
    [SerializeField] GameObject PordLaser;
    [SerializeField] ParticleSystem Spark;
    [Header("�÷��̾�")]
    [Space(10)]
    [SerializeField] GameObject Player;
    [Header("WorldCanvas(Pord)�� ����")]
    [SerializeField] GameObject MagicCircle;
    [SerializeField] LaserCoolTime LaserCoolTime;
    [Header("MainUI_Canvas�� ����")]
    [Space(10)]
    [SerializeField] GameObject Lockon;



    // �Ͽ��� ���� ����
    private bool isLockOn = false;
    private bool isMonster = false;
    private Collider target;

    // �Ͽ½� Ÿ�� ��ġ
    private Vector3 targetpos;

    //������Ʈ Ǯ���� ����
    private int bulletCount = 0;

    //�Ѿ� �����̿� ����
    private float BulletDealyTime = 0.1f;
    private float CurrentTime = 0f;

    //������ �����̿� ����
    private bool isLaser = false;

    //������ ��ġ�� ���� ����
    private Vector3 PlayerAround = new Vector3(1, 1, 0);
    private Vector3 MagicCirclePositon;

    //������ ������ ����� ����
    private bool isActive = false;

    //������ ����ġ�� ���� ����
    Vector3 ScreenCenter;

    private void Awake()
    {
        MagicCircle.SetActive(false);
    }
    private void FixedUpdate()
    {

        //if (transform.position.y < (Player.transform.position + TopPosition).y && !isActive)
        //{
        //    transform.position += Vector3.up * 0.2f * Time.deltaTime;
        //}

        ScreenCenter = Camera.main.ScreenToWorldPoint(new Vector3(960, 0, 840));
        LaserCoolTime.transform.rotation = Cam.transform.rotation;
        MagicCircle.transform.rotation = Cam.transform.rotation;

        // ------------------------��ǲ ----------------------------
        if (Input.GetKey(KeyCode.RightShift) && !isLaser ||
            Input.GetKey(KeyCode.LeftShift) && !isLaser) //�������� ���ÿ� �����°� ����
        {
            CurrentTime += Time.deltaTime;
            if (BulletDealyTime > CurrentTime)
            {
                return; //������ �ð������� �Ѿ��� �����
            }
            MagicCirclePositon = transform.forward*0.4f;
            isActive = true;
            //transform.position = new Vector3(transform.position.x, Player.transform.position.y + PlayerAround.y, transform.position.z);

            MagicCircle.transform.position = transform.position + MagicCirclePositon;
            MagicCircle.SetActive(true);

            PordBullet.Bullet[bulletCount].transform.position = transform.position + MagicCirclePositon;
            PordBullet.Bullet[bulletCount].SetActive(true);

            Spark.Play();
            Spark.transform.position = transform.position + MagicCirclePositon;


            AudioManager.Instance.PlaySfx(Define.SFX.PordBulletShot);
            if (isLockOn) //�Ͽ½� Ÿ�ٹ�������
            {
                PordBullet.Bullet[bulletCount].GetComponent<PordBulletMovement>().MoveRockOn((targetpos - PordBullet.Bullet[bulletCount].transform.position).normalized);
            }
            else //�Ͽ��� �ƴҽ� ������
            {

                PordBullet.Bullet[bulletCount].GetComponent<PordBulletMovement>().Move(transform.forward);
                //���� ���� �ʿ� �ӽ÷� �־����
            }

            //�Ҹ��� ���⿡ �־��ּ���
            Invoke("ActiveFalse", 0.3f);
            bulletCount++;
            if (bulletCount >= 60) // BulletSpawn ���� ������ ������ŭ
            {
                bulletCount = 0;
            }
            CurrentTime = 0;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) ||
            Input.GetKeyUp(KeyCode.RightShift))  // Ű�� ���� �� ������ �������
        {
            MagicCircle.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q)) // �Ͽ�
        {

            if (isLockOn) //�Ͽ��Ͻ� �Ͽ�����
            {
                isLockOn = false;
                Lockon.SetActive(false);
            }
            else if (isMonster) //�Ͽ��� �ƴҽ� , �ֺ��� ���Ͱ��ִ�(�Ͽ°��ɻ���)��� �Ͽ�
            {
                isLockOn = true;
                Lockon.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && LaserCoolTime.CanLaser) // 1�Է½� ������ ��Ÿ���� �ƴ϶��
        {

            StartCoroutine(Laser_co());
            LaserCoolTime.gameObject.SetActive(true);


        }
        if (!isLaser)
        {

            transform.position = Player.transform.position + PlayerAround;
            transform.rotation = Cam.transform.rotation;
        }
        //transform.position = new Vector3(Player.transform.position.x + Cam.transform.rotation.x,
        //                                 transform.position.y,
        //                                 Player.transform.position.z + Cam.transform.rotation.z);
        //transform.position = Player.transform.position + PlayerAround;
        //transform.rotation = Cam.transform.rotation;


    }
 
    private IEnumerator Laser_co() // ������ ���� �ڷ�ƾ
    {
        //ī�޶� ��¦ �����ּ���
        //�Ҹ��� ���⿡ �־��ּ���
        //transform.position = new Vector3(transform.position.x, Player.transform.position.y + PlayerAround.y, transform.position.z);
        AudioManager.Instance.PlaySfx(Define.SFX.PordLaser);
        isActive = true;
        isLaser = true;
        PordLaser.SetActive(true);
        PordLaser.transform.position = transform.position + MagicCirclePositon;


        MagicCircle.SetActive(true);
        MagicCircle.transform.position = transform.position + MagicCirclePositon;
        MagicCircle.transform.rotation = Cam.transform.rotation;

        if (isLockOn) //�Ͽ½� Ÿ�� ���� ���� ����
        {
            PordLaser.transform.LookAt(targetpos);
        }
        else //�Ͽ��� �ƴҽ� ������ ������
        {
            Vector3 LaserLotation = Cam.transform.rotation.eulerAngles;
            LaserLotation = new Vector3(0, LaserLotation.y, 0);
            PordLaser.transform.rotation = Quaternion.Euler(LaserLotation);

        }

        yield return new WaitForSeconds(1f);
        // �������� ���� ���� 
        PordLaser.SetActive(false);
        MagicCircle.SetActive(false);
        isLaser = false;
        ActiveFalse();
    }
    private void OnTriggerStay(Collider other) // �����ȿ� �������� �Ͽ� ���ɼ���, �Ͽ½� Ÿ����ġ ����
    {
        if (other.CompareTag("Enemy"))
        {
            isMonster = true;
            target = other;
            if (isLockOn && other == target)
            {
                targetpos = other.transform.position;
            }
        }

    }
    private void OnTriggerExit(Collider other) // ���� ������ ������ �Ͽ� ����
    {
        if (target != null)
        {

            if ((other == target && other.CompareTag("Enemy")) || !target.gameObject.activeSelf)
            {
                Debug.Log("���� ����������");
                isMonster = false;
                isLockOn = false;
                Lockon.SetActive(false);
                target = null;
            }

        }
    }


    public void ActiveFalse()
    {
        isActive = false;
    }
}

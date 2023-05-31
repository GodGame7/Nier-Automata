using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordControl : MonoBehaviour
{
    [Header("������ ���� �־��ּ���")]
    [SerializeField] Camera Cam;
    [SerializeField] PordBulletSpawn PordBullet;
    [SerializeField] GameObject PordLaser;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Lockon;
    [SerializeField] GameObject MagicCircle;
    [SerializeField] LaserCoolTime LaserCoolTime;
    [SerializeField] ParticleSystem Smoke;


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

    //������ ��ġ�� ���� ����
    private Vector3 PlayerAround = new Vector3(1, 1, 0);
    private Vector3 TopPosition = new Vector3(0, 1.8f, 0);
    private Vector3 MagicCirclePositon = new Vector3(0, 0, 0.4f);

    //������ ������ ����� ����
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

        // ------------------------��ǲ�Ŵ����� �ѱ�κ� ----------------------------
        if (Input.GetKey(KeyCode.RightShift) && !isLaser ||
            Input.GetKey(KeyCode.LeftShift) && !isLaser) //�������� ���ÿ� �����°� ����
        {
            CurrentTime += Time.deltaTime;
            if (BulletDealyTime > CurrentTime)
            {
                return; //������ �ð������� �Ѿ��� �����
            }

            isActive = true;
            transform.position = Player.transform.position + PlayerAround;

            MagicCircle.transform.position = transform.position + MagicCirclePositon;
            MagicCircle.SetActive(true);

            PordBullet.Bullet[bulletCount].transform.position = transform.position + MagicCirclePositon;
            PordBullet.Bullet[bulletCount].SetActive(true);

            Smoke.Play();
            Smoke.transform.position = transform.position + MagicCirclePositon;

            if (isLockOn) //�Ͽ½� Ÿ�ٹ�������
            {
                PordBullet.Bullet[bulletCount].GetComponent<PordBulletMovement>().Move((targetpos-PordBullet.Bullet[bulletCount].transform.position).normalized);
            }
            else //�Ͽ��� �ƴҽ� ������
            {
                PordBullet.Bullet[bulletCount].GetComponent<PordBulletMovement>().Move(-Cam.transform.position.normalized);
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

        //-- �÷��̾� �����ӿ� ���缭 ���嵵 ������--

        //�ӵ� �÷��̾�� �����ַ�
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, 1) * 5f * Time.deltaTime;
            //�ӽð� , ���� �÷��̾� �̵��ӵ��� �����̴°ź��� ���濹��
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -1) * 5f * Time.deltaTime;
            //�ӽð� , ���� �÷��̾� �̵��ӵ��� �����̴°ź��� ���濹��
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0) * 5f * Time.deltaTime;
            //�ӽð� , ���� �÷��̾� �̵��ӵ��� �����̴°ź��� ���濹��
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0) * 5f * Time.deltaTime;
            //�ӽð� , ���� �÷��̾� �̵��ӵ��� �����̴°ź��� ���濹��
        }
        // ----------------------------������� -----------------------------

    }

    private IEnumerator Laser_co() // ������ ���� �ڷ�ƾ
    {
        //ī�޶� ��¦ �����ּ���
        //�Ҹ��� ���⿡ �־��ּ���
        transform.position = Player.transform.position + PlayerAround;
        isActive = true;
        isLaser = true;
        PordLaser.SetActive(true);
        MagicCircle.SetActive(true);
        PordLaser.transform.position = transform.position + MagicCirclePositon;
        if (isLockOn) //�Ͽ½� Ÿ�� ���� ���� ����
        {
            PordLaser.transform.LookAt(targetpos);
        }
        else //�Ͽ��� �ƴҽ� ������ ������
        {
            PordLaser.transform.rotation = Quaternion.Euler(-Cam.transform.position.normalized);
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

            if (isLockOn)
            {
                targetpos = other.transform.position;
            }
        }

    }
    private void OnTriggerExit(Collider other) // ���� ������ ������ �Ͽ� ����
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

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
        if (Input.GetKey(KeyCode.RightShift) && !isLaser || Input.GetKey(KeyCode.LeftShift) && !isLaser) //�������� ���ÿ� �����°� ����
        {
            CurrentTime += Time.deltaTime;
            if (BulletDealyTime > CurrentTime)
            {
                return; //������ �ð������� �Ѿ��� �����
            }
            transform.position = Player.transform.position + PlayerAround;
            isActive = true;
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
            Invoke("ActiveFalse",0.3f);
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
            //���� �����ʿ� �ӽ÷� �־����
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

    private IEnumerator Laser_co()
    {
        //ī�޶� ��¦ �����ּ���
        //�Ҹ��� ���⿡ �־��ּ���
        transform.position = Player.transform.position + PlayerAround;
        isActive = true;
        isLaser = true;
        PordLaser.SetActive(true);
        PordLaser.transform.position = transform.position;
        PordLaser.transform.rotation = Quaternion.Euler(-Cam.transform.position.normalized);
        yield return new WaitForSeconds(1f);
        PordLaser.SetActive(false);
        isLaser = false;
        ActiveFalse();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isMonster = true;

            if (isLockOn)
            {
                targetpos = other.transform.position.normalized;
            }
        }

    }
    
    public void ActiveFalse()
    {
        isActive = false;
    }
}

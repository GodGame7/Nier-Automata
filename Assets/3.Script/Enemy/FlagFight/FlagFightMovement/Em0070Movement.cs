using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Em0070Movement : MonoBehaviour
{

    [Header("�� ����")]
    [SerializeField] Animator animator;
    [SerializeField] Vector3 firstDestPos;
    [SerializeField] float fireDelay = 1.0f;
    [SerializeField] float DashSpeed = 0.5f;

    [Space(0.5f)]
    [Header("�Ѿ�")]
    [SerializeField] GameObject bulletHard;
    [SerializeField] GameObject bulletSoft;
    [SerializeField] float bulletSpeed = 0.20f;

    [Space(0.5f)]
    [Header("Ȯ�ο�")]
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform playerTransform;
    [SerializeField] FlagEmInformation flagEmInformation;


    public UnityEvent Ready;
    public UnityEvent StopSlowSpin;
    public UnityEvent SlowSpin;

    // Start is called before the first frame update
    void Start()
    {
        flagEmInformation = GetComponent<FlagEmInformation>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("�÷��̾� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }


    // ȥ�� ������ ��
    void Alone()
    {
        StartCoroutine(Fire_co());
    }

    private IEnumerator Fire_co()
    {
        while (true)
        {
            GameObject Bullet = bulletSoft;
            int bulletType = Random.Range(0, 4);
            if (bulletType == 0)
            {
                Bullet = bulletHard;
            }
            Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject bullet = Instantiate(Bullet, bulletPosition, transform.rotation);
            Vector3 direction = (playerObject.transform.position - bullet.transform.position).normalized;
            bullet.transform.LookAt(playerObject.transform);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = direction * bulletSpeed;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}

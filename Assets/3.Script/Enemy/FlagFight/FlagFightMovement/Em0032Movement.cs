using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Em0032Movement : MonoBehaviour
{
    [Header("적 0032 정보")]
    [SerializeField] float maxHp = 50f;
    [SerializeField] float firstMoveSpeed = 2.0f;
    [SerializeField] float lastMoveSpeed = 0.5f;
    [SerializeField] float rotateSpeed = 60.0f;
    [SerializeField] float fireDelay = 1.0f;

    [Space(0.5f)]
    [Header("총알")]
    [SerializeField] GameObject bulletHard;
    [SerializeField] GameObject bulletSoft;
    [SerializeField] float bulletSpeed = 1.0f;

    [Space(0.5f)]
    [Header("Enemy Spawner에서 정해주어야 할 것")]
    [SerializeField] public Vector3 firstDesPos;
    [SerializeField] public bool isCanLook;

    [Space(0.5f)]
    [Header("확인용")]
    [SerializeField] bool isReady = false;
    [SerializeField] float fireTimer;
    [SerializeField] Vector3 desPos;
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform playerTransform;
    [SerializeField] FlagEmInformation flagEmInformation;

    /*start는 확인용이니, 에너미 스폰 생성시 삭제할것.*/
    private void Start()
    {

        desPos = firstDesPos;
        fireTimer = 0.0f;
        StartCoroutine(Move_co());
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("플레이어 오브젝트를 찾을 수 없습니다.");
        }
    }

    private void FixedUpdate()
    {
        // Todo... isReady이후에 화면 밖으로 나가지 않도록 조정.
    }


    private void Update()
    {
        fireTimer += Time.deltaTime;
        // 죽거나 준비되지 않았으면 return
        if (!isReady || flagEmInformation.isDie)
        {
            return;
        }
        // 플레이어를 처다볼 수 있다면 회전
        if (playerTransform != null && isCanLook)
        {
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
        if (fireDelay >= fireTimer)
        {
            return;
        }
        fireTimer = 0.0f;
        Fire();
    }

    private IEnumerator Move_co()
    {
        while (Vector3.SqrMagnitude(transform.position - desPos) >= 0.00005f)
        {
            if (!flagEmInformation.isDie)
            {
                transform.position = Vector3.MoveTowards(transform.position, desPos, firstMoveSpeed * Time.deltaTime);
            }
            yield return null;
        }
        transform.position = desPos;
        isReady = true;
        while (!flagEmInformation.isDie)
        {
            transform.RotateAround(playerTransform.position, playerTransform.up, lastMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void Fire()
    {
        GameObject Bullet = bulletSoft;
        int bulletType = Random.Range(0, 4);
        if (bulletType == 0)
        {
            Bullet = bulletHard;
        }
        GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);
        Vector3 direction = (playerObject.transform.position - bullet.transform.position).normalized;
        bullet.transform.LookAt(playerObject.transform);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }
}

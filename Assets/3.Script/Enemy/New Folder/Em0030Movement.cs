using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Em0030Movement : MonoBehaviour
{
    [Header("적03 정보")]
    [SerializeField] float maxHp = 50f;
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float rotateSpeed = 60.0f; 
    [SerializeField] float fireDelay = 3.0f;

    [Space(0.5f)]
    [Header("총알")]
    [SerializeField] GameObject bulletHard;
    [SerializeField] GameObject bulletSoft;

    [Space(0.5f)]
    [Header("Enemy Spawner에서 정해주어야 할 것")]
    [SerializeField] Vector3 firstDesPos;
    [SerializeField] Vector3 lastDesPos;
    [SerializeField] bool isCanLook;

    [Space(0.5f)]
    [Header("확인용")]
    [SerializeField] bool isReady = false;
    [SerializeField] bool isDie = false; 
    [SerializeField] float currentHp;
    [SerializeField] float fireTimer;
    [SerializeField] Vector3 desPos;
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform playerTransform;

    /*start는 확인용이니, 에너미 스폰 생성시 삭제할것.*/

    private void Start()
    {
        desPos = firstDesPos;
        currentHp = maxHp;
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

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (!isReady || isDie)
        {
            return;
        }
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
        StartCoroutine(Fire_co());
    }

    private IEnumerator Move_co()
    {
        while (Vector3.SqrMagnitude(transform.position - desPos) >= 0.00005f)    
        {
            if (!isDie)
            {
                transform.position = Vector3.MoveTowards(transform.position, desPos, moveSpeed * Time.deltaTime);
            }
            yield return null;
        }
        transform.position = desPos;
        isReady = true;
        desPos = lastDesPos;
    }

    private IEnumerator Fire_co()
    {
        Debug.Log("파이어!");
        yield return null;
    }

    private void Die()
    {
        Debug.Log("죽음");
    }
}

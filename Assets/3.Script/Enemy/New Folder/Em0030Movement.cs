using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Em0030Movement : MonoBehaviour
{
    [Header("��03 ����")]
    [SerializeField] float maxHp = 50f;
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float rotateSpeed = 60.0f; 
    [SerializeField] float fireDelay = 3.0f;

    [Space(0.5f)]
    [Header("�Ѿ�")]
    [SerializeField] GameObject bulletHard;
    [SerializeField] GameObject bulletSoft;

    [Space(0.5f)]
    [Header("Enemy Spawner���� �����־�� �� ��")]
    [SerializeField] Vector3 firstDesPos;
    [SerializeField] Vector3 lastDesPos;
    [SerializeField] bool isCanLook;

    [Space(0.5f)]
    [Header("Ȯ�ο�")]
    [SerializeField] bool isReady = false;
    [SerializeField] bool isDie = false; 
    [SerializeField] float currentHp;
    [SerializeField] float fireTimer;
    [SerializeField] Vector3 desPos;
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform playerTransform;

    /*start�� Ȯ�ο��̴�, ���ʹ� ���� ������ �����Ұ�.*/

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
            Debug.LogError("�÷��̾� ������Ʈ�� ã�� �� �����ϴ�.");
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
        Debug.Log("���̾�!");
        yield return null;
    }

    private void Die()
    {
        Debug.Log("����");
    }
}

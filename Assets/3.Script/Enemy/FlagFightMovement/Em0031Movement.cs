using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Em0031Movement : MonoBehaviour
{
    [Header("�� 0031 ����")]
    [SerializeField] float maxHp = 50f;
    [SerializeField] float firstMoveSpeed = 2.0f;

    [Space(0.5f)]
    [Header("Enemy Spawner���� �����־�� �� ��")]
    [SerializeField] public Vector3 firstDesPos;

    [Space(0.5f)]
    [Header("Ȯ�ο�")]
    [SerializeField] bool isDie = false;
    [SerializeField] float currentHp;
    [SerializeField] Vector3 desPos;
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform playerTransform;

    private void Start()
    {
        desPos = firstDesPos;
        currentHp = maxHp;
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

    private IEnumerator Move_co()
    {
        while (Vector3.SqrMagnitude(transform.position - desPos) >= 0.00005f)
        {
            if (!isDie)
            {
                transform.position = Vector3.MoveTowards(transform.position, desPos, firstMoveSpeed * Time.deltaTime);
            }
            yield return null;
        }
        transform.position = desPos;
    }

    public void OnDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDie = true;
        Debug.Log("����");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Em0031Movement : MonoBehaviour
{
    [Header("�� 0031 ����")]
    [SerializeField] float firstMoveSpeed = 2.0f;

    [Space(0.5f)]
    [Header("Enemy Spawner���� �����־�� �� ��")]
    [SerializeField] public Vector3 firstDesPos;

    [Space(0.5f)]
    [Header("Ȯ�ο�")]
    [SerializeField] Vector3 desPos;
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform playerTransform;
    [SerializeField] FlagEmInformation flagEmInformation;

    private void Start()
    {
        desPos = firstDesPos;
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

    private void OnEnable()
    {
        desPos = firstDesPos;
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
            if (!flagEmInformation.isDie)
            {
                if (0.50f < transform.position.x || -0.50f > transform.position.x
                    || 0.50f < transform.position.y || - 0.50f > transform.position.y
                    || 0.50f < transform.position.z || - 0.50f > transform.position.z)
                {
                    flagEmInformation.Disappear();
                }
                transform.position = Vector3.MoveTowards(transform.position, desPos, firstMoveSpeed * Time.deltaTime);
            }
            yield return null;
        }
        transform.position = desPos;
    }
}

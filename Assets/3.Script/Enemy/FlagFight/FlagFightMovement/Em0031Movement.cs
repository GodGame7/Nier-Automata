using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Em0031Movement : MonoBehaviour
{
    [Header("�� 0031 ����")]
    [SerializeField] float firstMoveSpeed = 2.0f;

    [Space(0.5f)]
    [Header("Enemy Spawner���� �����־�� �� ��")]
    [SerializeField] public Vector3 desPos;

    [Space(0.5f)]
    [Header("Ȯ�ο�")]
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform playerTransform;
    [SerializeField] FlagEmInformation flagEmInformation;
    [SerializeField] float speed;

    private void Start()
    {
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

    public IEnumerator Move_co()
    {
        speed = firstMoveSpeed;
        while (Vector3.SqrMagnitude(transform.position - desPos) >= 0.00005f)
        {
            if (!flagEmInformation.isDie)
            {
                if (0.35f < transform.position.x || -0.35f > transform.position.x
                    || 0.35f < transform.position.y || -0.35f > transform.position.y
                    || 0.35f < transform.position.z || -0.35f > transform.position.z)
                {
                    transform.position = Vector3.zero;
                    flagEmInformation.Disappear();
                }
                transform.position = Vector3.MoveTowards(transform.position, desPos, speed * Time.deltaTime);
            }
            yield return null;
        }
        transform.position = desPos;
    }
}

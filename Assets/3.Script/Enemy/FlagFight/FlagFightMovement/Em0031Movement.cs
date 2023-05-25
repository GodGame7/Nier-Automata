using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Em0031Movement : MonoBehaviour
{
    [Header("적 0031 정보")]
    [SerializeField] float firstMoveSpeed = 2.0f;

    [Space(0.5f)]
    [Header("Enemy Spawner에서 정해주어야 할 것")]
    [SerializeField] public Vector3 firstDesPos;

    [Space(0.5f)]
    [Header("확인용")]
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
            Debug.LogError("플레이어 오브젝트를 찾을 수 없습니다.");
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
            Debug.LogError("플레이어 오브젝트를 찾을 수 없습니다.");
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

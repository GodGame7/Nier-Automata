using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Em0071Movement : MonoBehaviour
{
    [Header("�� ����")]
    [SerializeField] float fireDelay = 0.3f;
    [SerializeField] Animator animator;
    [SerializeField] Em0070Movement em0070Movement;

    [Space(0.5f)]
    [Header("�Ѿ�")]
    [SerializeField] GameObject bulletHard;
    [SerializeField] GameObject bulletSoft;
    [SerializeField] float bulletSpeed = 0.05f;
    [SerializeField] bool isFront = true;

    [Space(0.5f)]
    [Header("Ȯ�ο�")]
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform playerTransform;
    [SerializeField] FlagEmInformation flagEmInformation;

    WaitForSeconds waitfireDelay;

    private void Start()
    {
        fireDelay = 0.1f;
        bulletSpeed = 0.08f;

        waitfireDelay = new WaitForSeconds(fireDelay);
        flagEmInformation = GetComponent<FlagEmInformation>();
        em0070Movement = FindObjectOfType<Em0070Movement>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("�÷��̾� ������Ʈ�� ã�� �� �����ϴ�.");
        }

        em0070Movement.Ready.AddListener(Ready);
        em0070Movement.SlowSpin.AddListener(SlowSpin);
    }

    private void Ready()
    {
        animator.SetTrigger("Ready");
    }

    private void SlowSpin()
    {
        StartCoroutine(Fire_co());
    }

    private IEnumerator Fire_co()
    {
        float counter = 0.0f;
        while (counter < 5.0f)
        {
            GameObject Bullet = bulletSoft;
            int bulletType = Random.Range(0, 4);
            if (bulletType == 0)
            {
                Bullet = bulletHard;
            }
            Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject bullet = Instantiate(Bullet, bulletPosition, transform.rotation);
            Vector3 direction = transform.forward;
            if (!isFront)
            {
                direction = -transform.forward;
            }
            bullet.transform.LookAt(bullet.transform.position + direction);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = direction * bulletSpeed;
            }
            counter += fireDelay;
            yield return waitfireDelay;
        }
    }
}
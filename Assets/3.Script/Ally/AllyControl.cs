using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyControl : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rigid;
    private FlagBulletSpawner[] bulletSpawners = new FlagBulletSpawner[2];
    private float lastFireTime = 0.0f;
    [SerializeField]
    private float fireDelay = 0.2f;

    private float speed = 0.2f;
    [SerializeField]
    private int dir = 1;
    [SerializeField]
    private float time = 0f;
    private float stopDirTime = 0.5f;
    private float changeDirTime = 2.0f;
    public bool isSway = true;

    Vector3 newPosition;

    WaitForSeconds fireDelay_wait;

    private void Awake()
    {
        TryGetComponent(out anim);
        TryGetComponent(out rigid);
        bulletSpawners = GetComponentsInChildren<FlagBulletSpawner>();

        fireDelay_wait = new WaitForSeconds(fireDelay);

        newPosition = new Vector3(
        Mathf.Clamp((rigid.position.x + dir * 0.1f), -0.3f, 0.3f), 0, 0);

        StartCoroutine(Fire_co(3f));
        StartCoroutine(MoveTo(0.28f));
    }

    private void Update()
    {
        time += Time.deltaTime;
        //Sway();
        //Fire();
    }

    public IEnumerator MoveTo(float destX)
    {
        Vector3 destPos = destX * Vector3.right;
        int cnt = 0;
        while (Vector3.SqrMagnitude(destPos - rigid.position) > 0.0001f)
        {
            rigid.position = Vector3.Lerp(rigid.position, destPos, Time.deltaTime * (1 + cnt++ * 0.01f));
            yield return null;
        }
        rigid.position = destPos;
    }

    private void Sway()
    {
        if (isSway)
        {
            if (time > stopDirTime)
            {
                rigid.position = Vector3.Lerp(rigid.position, newPosition, Time.deltaTime);
                anim.SetFloat("hSpeed", dir);
            }
            else
            {
                anim.SetFloat("hSpeed", 0);
            }
            if (time > changeDirTime + stopDirTime)
            {
                dir *= -1;
                time = 0;

                newPosition = new Vector3(
                Mathf.Clamp((rigid.position.x + dir * 0.1f), -0.3f, 0.3f), 0, 0);
            }
        }
        else
        {
            anim.SetFloat("hSpeed", 0);
            time = 0;
        }
    }
    public void Fire()
    {
        if (Time.time > lastFireTime + fireDelay)
        {
            foreach (FlagBulletSpawner b in bulletSpawners)
            {
                b.Fire();
            }
            lastFireTime = Time.time;
        }
    }
    public IEnumerator Fire_co(float time)
    {
        float startTime = Time.time;
        while (true)
        {
            yield return fireDelay_wait;

            foreach (FlagBulletSpawner b in bulletSpawners)
            {
                b.Fire();
            }
            if (Time.time - startTime >= time)
            {
                break;
            }
        }
    }

    public void Transform()
    {
        anim.SetTrigger("toGundam");
    }

    public void LookAt()
    {

    }
}

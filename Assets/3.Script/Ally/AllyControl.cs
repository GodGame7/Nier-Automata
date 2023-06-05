using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AllyControl : MonoBehaviour
{

    private GameObject[] enemies;
    private List<GameObject> aliveEnemies = new List<GameObject>();
    int enemyIndex;

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
    public bool isSway = false;
    public bool isFire = false;

    Vector3 newPosition;

    WaitForSeconds fireDelay_wait;

    private void Awake()
    {
        TryGetComponent(out anim);
        TryGetComponent(out rigid);
        bulletSpawners = GetComponentsInChildren<FlagBulletSpawner>();
        isSway = false;
        isFire = false;
        fireDelay_wait = new WaitForSeconds(fireDelay);
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Sway();
        Fire();
    }

    public void MoveTo(string destPos)
    {
        StartCoroutine(MoveTo_co(destPos));
    }
    private IEnumerator MoveTo_co(string destPosition)
    {
        string[] pos = destPosition.Split(',');
        Vector3 destPos = new Vector3(float.Parse(pos[0].Trim()), 0, float.Parse(pos[1].Trim()));
        int cnt = 0;
        while (Vector3.SqrMagnitude(destPos - rigid.position) > 0.000001f)
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

    // isFire가 true일 때 발사
    public void Fire()
    {
        if (Time.time > lastFireTime + fireDelay && isFire)
        {
            foreach (FlagBulletSpawner b in bulletSpawners)
            {
                b.Fire();
            }
            lastFireTime = Time.time;
        }
    }
    // time 동안 쏘고 중지
    //public IEnumerator Fire_co(float time)
    //{
    //
    //    float startTime = Time.time;
    //    while (true)
    //    {
    //        yield return fireDelay_wait;

    //        foreach (FlagBulletSpawner b in bulletSpawners)
    //        {
    //            b.Fire();
    //        }
    //        if (Time.time - startTime >= time)
    //        {
    //            break;
    //        }
    //    }
    //}

    public void Transform()
    {
        anim.SetTrigger("toGundam");
        isFire = false;
        isSway = false;
        StartCoroutine(FindEnemies_co());
    }
    private IEnumerator FindEnemies_co()
    {
        yield return new WaitForSeconds(0.5f);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("ToGundam") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f);
        StartCoroutine(LookAt_co());
    }

    private IEnumerator LookAt_co()
    {
        foreach (GameObject enemy in enemies)
        {
            aliveEnemies.Add(enemy);
        }
        StartCoroutine(nameof(SearchEnemy));

        while (true)
        {
            List<GameObject> tmpList = new List<GameObject>();
            int length = aliveEnemies.Count;
            for (int i = 0; i < length; i++)
            {
                if (!aliveEnemies[i].activeSelf)
                {
                    aliveEnemies.Remove(aliveEnemies[i]);
                    break;
                }
            }
            if (aliveEnemies.Count == 0)
            {
                StopCoroutine(nameof(SearchEnemy));
                break;
            }
            transform.LookAt(enemies[enemyIndex].transform);
            Quaternion target = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = target;
            yield return null;
        }
    }

    private IEnumerator SearchEnemy()
    {
        while (true)
        {
            enemyIndex = Random.Range(0, aliveEnemies.Count);
            yield return new WaitForSeconds(1f);
        }
    }

    public void StartSway()
    {
        isSway = true;
    }

    public void StopSway()
    {
        isSway = false;
    }

    public void StartFire()
    {
        isFire = true;
    }

    public void StopFire()
    {
        isFire = false;
    }
}

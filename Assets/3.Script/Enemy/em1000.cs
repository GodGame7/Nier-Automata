using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class em1000 : Enemy
{

    [Header("톱 관련")]
    [Space(10f)]
    [SerializeField] GameObject saw;
    [Range(1f, 500f)]
    [SerializeField] float maxsawrotateSpeed = 500f;
    [Range(1f, 500f)]
    [SerializeField] float minsawrotateSpeed = 150f;
    [SerializeField] float speed_down_time = 5f;
    [SerializeField] bool isattack = false;


    private float currentSawRotateSpeed;

    void Start()
    {
        currentSawRotateSpeed = maxsawrotateSpeed;

        if (saw == null)
        {
            Debug.Log("saw 할당 안되어있다. ");
            saw = GameObject.Find("bone-1/bone000/bone001/bone002/bone003/bone004/bone005/bone007/bone008/bone009");
        }
    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (!enemyHp.isdead)
        {
            saw.transform.Rotate(-currentSawRotateSpeed * Time.deltaTime, 0, 0);
            SawAttack();
        }
    }


    //공격
    void SawAttack()
    {
        if (currentSawRotateSpeed == maxsawrotateSpeed && isattack)

        {
            StartCoroutine(DecreaseRotationSpeed());
        }
        else if (!isattack)
        {
            currentSawRotateSpeed = maxsawrotateSpeed;
        }
    }


    //회전 떨구기
    IEnumerator DecreaseRotationSpeed()
    {
        float Timer = 0f;
        float startSpeed = currentSawRotateSpeed;

        //2초 정도 속도 줄어듬
        while (Timer < speed_down_time)
        {
            Timer += Time.deltaTime;

            currentSawRotateSpeed = Mathf.Lerp(startSpeed, minsawrotateSpeed, Timer / speed_down_time);
            yield return null;
        }

        currentSawRotateSpeed = minsawrotateSpeed;
    }

    public override IEnumerator Die()
    {
        anim.SetTrigger("DieTrigger");
        anim.SetBool("Die", true);

        if (enemyHp.capsuleCollider != null)
        {
            enemyHp.capsuleCollider.enabled = false;
        }

        //yield return new WaitUntil(() => anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("SawUp"));
        enemyHp.isdead_effect.SetActive(true);

        yield return new WaitForSeconds(3f);
        //enemyHp.isdead = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class em1000 : Enemy
{
    enum Dir
    {
        Left,
        Center,
        Right
    }

    [Header("�� ����")]
    [Space(10f)]
    [SerializeField] GameObject saw;
    [Range(1f, 500f)]
    [SerializeField] float maxsawrotateSpeed = 500f;
    [Range(1f, 500f)]
    [SerializeField] float minsawrotateSpeed = 150f;
    [SerializeField] float speed_down_time = 5f;
    [SerializeField] bool isattack = false;

    private float target_x;
    private float currentSawRotateSpeed;

    bool allattack;
    bool allattack2;

    Dir dir;

    void Start()
    {
        if (saw == null)
        {
            Debug.Log("saw �Ҵ� �ȵǾ��ִ�. ");
            saw = GameObject.Find("bone-1/bone000/bone001/bone002/bone003/bone004/bone005/bone007/bone008/bone009");
        }


        currentSawRotateSpeed = maxsawrotateSpeed;

        anim.SetTrigger("Intro");
        StartCoroutine(Attack());

    }

    private void FixedUpdate()
    {
        //�鳯 ȸ��
        Saw();

        TargetCheck();

    }

    IEnumerator Attack()
    {
        Debug.Log("�ڷ�ƾ �餩���");
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Attack(All)go"));
        yield return null;


        Debug.Log("����");

        while (true)
        {

            switch (dir)
            {
                case Dir.Left:
                    anim.SetBool("Left", true);
                    anim.SetBool("Center", false);
                    anim.SetBool("Right", false);
                    break;

                case Dir.Center:
                    anim.SetBool("Left", false);
                    anim.SetBool("Center", true);
                    anim.SetBool("Right", false);
                    break;

                case Dir.Right:
                    anim.SetBool("Left", false);
                    anim.SetBool("Center", false);
                    anim.SetBool("Right", true);
                    break;
            }

            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
            anim.SetFloat("Random", Random.Range(0, 2));
            isattack = false;
            Debug.Log("normaliziedTimer �Ϸ�");
            yield return null;

            //yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Attack(All)go"));
            yield return new WaitUntil(() => anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Attack"));
            isattack = true;
            yield return null;
            Debug.Log("AttackAllgo��µ� ?");
        }
    }

    IEnumerator While()
    {
        while (true)
        {

            if (!allattack)
            {
                Debug.Log("1�� �ڷ�ƾ ����");
                StartCoroutine(Attack1());
                yield return null;
            }


            if (!allattack)
            {
                Debug.Log("2�� �ڷ�ƾ ����");
                StartCoroutine(Attack2());
                yield return null;
            }

            Debug.Log("�ݺ�");
            yield return null;
        }
    }

    IEnumerator Attack1()
    {
        Debug.Log("1�� �ڷ�ƾ �Ծ� ?");

        //isattack = true;

        allattack = true;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Attack(All)go") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);


        Debug.Log("������?");

        //isattack = false;

        allattack = false;

    }

    IEnumerator Attack2()
    {
        Debug.Log("2�� �ڷ�ƾ ���Ծ�?");
        anim.SetFloat("Random", Random.Range(0, 2));

        switch (dir)
        {
            case Dir.Left:
                anim.SetBool("Left", true);
                anim.SetBool("Center", false);
                anim.SetBool("Right", false);
                break;

            case Dir.Center:
                anim.SetBool("Left", false);
                anim.SetBool("Center", true);
                anim.SetBool("Right", false);
                break;

            case Dir.Right:
                anim.SetBool("Left", false);
                anim.SetBool("Center", false);
                anim.SetBool("Right", true);
                break;
        }
        //isattack = true;
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Attack(All)go"));

    }



    //�¿� �Ǵ�
    void TargetCheck()
    {
        target_x = target.position.x;

        if (target_x > 80f)
        {
            dir = Dir.Left;
        }
        else if (target_x <= 80f && target_x > 53f)
        {
            dir = Dir.Center;
        }
        else if (target_x <= 53f)
        {
            dir = Dir.Right;
        }

    }

    //�鳯 ����
    void Saw()
    {
        if (!enemyHp.isdead)
        {
            //�鳯 ȸ��
            saw.transform.Rotate(-currentSawRotateSpeed * Time.deltaTime, 0, 0);

            //�ְ�ӵ��̰�, ������ ����Ǹ�, �� ȸ���ӵ��� õõ�� ��������
            if (currentSawRotateSpeed == maxsawrotateSpeed && isattack)
            {
                StartCoroutine(DecreaseRotationSpeed());
            }
            else if (!isattack)
            {
                currentSawRotateSpeed = maxsawrotateSpeed;
            }
        }
    }

    //�� ȸ���ӵ� �������ϱ�
    IEnumerator DecreaseRotationSpeed()
    {
        float Timer = 0f;
        float startSpeed = currentSawRotateSpeed;

        //2�� ���� �ӵ� �پ��
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
        enabled = false;
        //StopCoroutine(Attack());
        //enemyHp.isdead = false;
    }
}

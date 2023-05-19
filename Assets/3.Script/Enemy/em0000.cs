using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class em0000 : MonoBehaviour
{
    enum State
    {
        IDLE,
        WALK,
        JUMP,
        DASH,
        ATTACK,
        DIE
    }

    [SerializeField] private float Hp;
    public float hp
    {
        get { return Hp; }
        set
        {
            Hp = value;

            if (Hp <= 0)
            {
                StopCoroutine(CheckState());
                isdead = true;
                anim.SetTrigger("Die");
                state = State.DIE;
            }
        }
    }

    //Ÿ�ٰ��� �Ÿ�
    float distance;
    bool isdead = false;

    //BoxCollider[] boxCollider;

    //Ÿ��
    [SerializeField] Transform target;

    Animator anim;
    AnimatorClipInfo[] animatorinfo;

    //����
    [SerializeField] State state;

    Stopwatch sw = new Stopwatch();

    private void Awake()
    {
        TryGetComponent(out anim);
        //boxCollider = GetComponentsInChildren<BoxCollider>();
    }


    private void Start()
    {

        Hp = 100;

        //Ÿ�� ��ġ ã��
        target = GameObject.FindGameObjectWithTag("Player").transform;
        state = State.IDLE;

        //�ݶ��̴� ���� => �ٵ� �ȉ�
        //for (int i = 0; i < boxCollider.Length; i++)
        //{
        //    boxCollider[i].enabled = false;
        //}

        StartCoroutine(CheckState());
    }

    IEnumerator CheckState()
    {
        while (!isdead)
        {

            AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            animatorinfo = this.anim.GetCurrentAnimatorClipInfo(0);
            string current_animation = animatorinfo[0].clip.name;

            if (current_animation == "em0000_Attack End(Windmill)" || 
                current_animation == "em0000_Attack")
            {
                yield return null;
                continue;
            }



            // Ÿ�� ��ġ�� ���´�.
            Vector3 targetPosition = target.position;
            targetPosition.y = 0f;

            // Ÿ�� ������ �ٶ󺸵��� ȸ��

            transform.LookAt(targetPosition);


            //Ÿ�ٰ� �Ÿ��� ����
            distance = Vector3.Distance(transform.position, target.transform.position);

            switch (state)
            {
                case State.IDLE:
                    //1�� �ڿ� �ȱ� ����
                    yield return new WaitForSeconds(1f);
                    state = State.WALK;
                    break;

                case State.WALK:
                    UpdateWalk();
                    break;

                case State.JUMP:
                    break;

                case State.DASH:
                    UpdateDash();
                    break;

                case State.ATTACK:
                    Attack();
                    break;

            }
            yield return null;
        }

    }

    void UpdateWalk()
    {
        //�ȱ�
        anim.SetBool("Walk", true);

        //���� �Ÿ��� 3f ���ϸ� �����ϰ�, 6f �̻��̸� �뽬��
        if (distance < 2.8f)
        {
            state = State.ATTACK;
        }
        else if (distance >= 6f)
        {
            state = State.DASH;
        }
    }

    void Attack()
    {
        sw.Start();
        int random = Random.Range(1, 3);
        UnityEngine.Debug.Log(sw.ElapsedMilliseconds);

        switch (random)
        {
            case 1:
                WindmillAttack();
                break;
            case 2:
                PunchAttack();
                break;
        }
    }

    void WindmillAttack()
    {
        if (!anim.GetBool("Attack2"))
        {
            anim.SetBool("Attack2", true);
        }
        else if (anim.GetBool("Attack2") && (distance <= 2f || sw.ElapsedMilliseconds > 4000.0f))
        {
            anim.SetBool("Attack2", false);
            state = State.IDLE;
            sw.Reset();
        }

    }

    void PunchAttack()
    {
        if (!anim.GetBool("Attack2"))
        {
            anim.SetTrigger("Attack1");
            state = State.IDLE;
            sw.Reset();
        }
    }

    void UpdateDash()
    {
        anim.SetTrigger("Dash");
        state = State.WALK;
    }


    public void TakeDamage(float damage)
    {

    }
}

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

    //타겟과의 거리
    float distance;
    bool isdead = false;

    [SerializeField] BoxCollider[] boxCollider;

    //타겟
    [SerializeField] Transform target;

    Animator anim;

    //상태
    [SerializeField] State state;

    private void Awake()
    {
        TryGetComponent(out anim);
        boxCollider = GetComponentsInChildren<BoxCollider>();
    }   


    private void Start()
    {
        //타겟 위치 찾기
        target = GameObject.FindGameObjectWithTag("Player").transform;
        state = State.IDLE;

        //콜라이더 끄기 => 근데 안됌
        //for (int i = 0; i < boxCollider.Length; i++)
        //{
        //    boxCollider[i].enabled = false;
        //}

        StartCoroutine(CheckState());
    }

    Stopwatch sw = new Stopwatch();
    IEnumerator CheckState()
    {
        while (!isdead)
        {
            // 타겟 위치를 얻어온다.
            Vector3 targetPosition = target.position;
            targetPosition.y = 0f;

            // 타겟 방향을 바라보도록 회전
            transform.LookAt(targetPosition);

            //타겟과 거리를 측정
            distance = Vector3.Distance(transform.position, target.transform.position);

            switch (state)
            {
                case State.IDLE:
                    //1초 뒤에 걷기 시작
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
            //AnimatorStateInfo aniInfo = anim.GetCurrentAnimatorStateInfo(0);
            //if ( (aniInfo.IsName("em0000_Attack End(Windmill)") && aniInfo.normalizedTime >= 1) ||
            //    (aniInfo.IsName("em0000_Attack") && aniInfo.normalizedTime >= 1))
            //{
            //    boxCollider.enabled = false;
            //}
            yield return null;
        }

    }

    void UpdateWalk()
    {
        //걷기
        anim.SetBool("Walk", true);

        //남은 거리가 3f 이하면 공격하고, 6f 이상이면 대쉬함
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
        UnityEngine.Debug.Log(random);

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

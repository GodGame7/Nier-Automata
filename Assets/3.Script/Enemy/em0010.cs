using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class em0010 : MonoBehaviour
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

    //타겟과의 거리
    float distance;
    bool isdead = false;

    //BoxCollider[] boxCollider;

    //타겟
    [SerializeField] Transform target;

    Animator anim;
    AnimatorClipInfo[] animatorinfo;

    //상태
    [SerializeField] State state;

    private void Awake()
    {
        TryGetComponent(out anim);
        //boxCollider = GetComponentsInChildren<BoxCollider>();
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

    IEnumerator CheckState()
    {
        while (!isdead)
        {

            AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            animatorinfo = this.anim.GetCurrentAnimatorClipInfo(0);
            string current_animation = animatorinfo[0].clip.name;
            Debug.Log(current_animation);

            if (current_animation == "Attack_Double" ||
                current_animation == "Attack_Foot" ||
                current_animation == "Attack_Left" ||
                current_animation == "Attack_Right")
            {
                yield return null;
               continue;
            }



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
                    //UpdateDash();
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
        //걷기
        anim.SetBool("Run", true);

        //남은 거리가 3f 이하면 공격하고, 6f 이상이면 대쉬함
        if (distance < 2.8f)
        {
            anim.SetFloat("Patton", 0);
            anim.SetBool("Run", false);
            state = State.ATTACK;
        }
        //else if (distance >= 6f)
        //{
        //    state = State.DASH;
        //}
    }

    void Attack()
    {
        float value = Random.Range(1, 5);
        Debug.Log(value);

        if (anim.GetFloat("Patton") <= 0f)
        {
            anim.SetFloat("Patton", (float)value);
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
            {
                state = State.IDLE;
            }
        }
    }

    //void UpdateDash()
    //{
    //    anim.SetTrigger("Dash");
    //    state = State.WALK;
    //}


    public void TakeDamage(float damage)
    {

    }
}

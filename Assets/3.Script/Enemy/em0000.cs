using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class em0000 : Enemy
{
    private void Start()
    {
        StartCoroutine(CheckState());
    }
 
    IEnumerator CheckState()
    {
        while (!isdead)
        {
            //거리 확인
            Distance();

            //타겟 처다보기
            TargetLookat();

            Debug.Log(distance);

            switch (state)
            {
                case State.IDLE:
                    yield return StartCoroutine(UpdateIdle2());
                    break;

                case State.WALK:
                    UpdateWalk();
                    break;

                case State.ATTACK:
                    yield return StartCoroutine(UpdateAttack2(pattonNum));
                    break;

                case State.DASH:
                    yield return StartCoroutine(UpdateDash2());
                    break;
            }
            yield return null;
        }

    }

    protected override IEnumerator UpdateAttack2(int PattonNum)
    {
        state = State.ATTACK;

        Debug.Log("공격 시작입니다.");
        int random = Random.Range(1, PattonNum + 1);

        switch (random)
        {
            case 1:
                yield return StartCoroutine(WindmillAttack2());
                break;

            case 2:
                yield return StartCoroutine(PunchAttact2());
                break;
        }
        Debug.Log("나갑니다!");
        state = State.IDLE;
    }


    IEnumerator WindmillAttack2()
    {
        anim.SetBool("Attack2", true);
        yield return new WaitForSeconds(4f);
        anim.SetBool("Attack2", false);

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("em0000_Idle"));
    }

    IEnumerator PunchAttact2()
    {
        anim.SetTrigger("Attack1");

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("em0000_Idle"));
    }

}
